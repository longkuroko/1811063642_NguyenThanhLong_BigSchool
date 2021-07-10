using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers.Api
{
    public class CoursesController : ApiController
    {
        public ApplicationDbContext _dbContext { get; set; }

        public CoursesController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [HttpDelete]
        public IHttpActionResult Cancel (int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(p => p.Id == id && p.LecturerId == userId);

            if (course.IsCanceled)
            {
                return NotFound();
            }
            course.IsCanceled = true;

            //Add notification 
            var notification = new Notification()
            {
                DateTime = DateTime.Now,
                Course = course,
                NotificationType = NotificationType.CourseCanceled

            };

            var attendees = _dbContext.Attendances
                .Where(a => a.CourseId == course.Id)
                .Select(a => a.Attendee)
                .ToList();

            foreach(var attendee in attendees)
            {
                var userNotification = new UserNotification()
                {
                    User = attendee,
                    Notification = notification
                };
            }

            _dbContext.SaveChanges();

            return Ok();
        }
    }
}
