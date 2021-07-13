using BigSchool.DTOs;
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
    public class UnAttendController : ApiController
    {
        private ApplicationDbContext _dbContext;
        public UnAttendController()
        {
            _dbContext = new ApplicationDbContext();
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult UnAttend(AttendanceDtos attendanceDto)
        {
            var userId = User.Identity.GetUserId();

            var DelAttend = _dbContext.Attendances
                .FirstOrDefault(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId);

            if (DelAttend == null)
            {
                return BadRequest("The Attendance does not exsists !");
            }
            _dbContext.Attendances.Remove(DelAttend);
            _dbContext.SaveChanges();

            return Ok();

        }
    }
}
