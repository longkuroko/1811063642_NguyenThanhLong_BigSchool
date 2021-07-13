using BigSchool.DTOs;
using BigSchool.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BigSchool.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private ApplicationDbContext _dbContext;

        public AttendancesController()
        {
            _dbContext = new ApplicationDbContext();
        }
        [HttpPost]
        public IHttpActionResult Attend(AttendanceDtos attendanceDto)
        {
            var userId = User.Identity.GetUserId();
            if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDto.CourseId))
                return BadRequest("The Attendance already exists !");

            var attendance = new Attendance
            {
                CourseId = attendanceDto.CourseId,
                AttendeeId = userId
            };
            _dbContext.Attendances.Add(attendance);
            _dbContext.SaveChanges();

            return Ok();
        }
        //[HttpPost]
        //public IHttpActionResult Attend(AttendanceDtos attendanceDtos)
        //{
        //    var userId = User.Identity.GetUserId();
        //    if (_dbContext.Attendances.Any(a => a.AttendeeId == userId && a.CourseId == attendanceDtos.CourseId))
        //        return BadRequest("The Attendance already Exists!");

        //    if (_dbContext.Attendances.Any(x => x.AttendeeId == userId && x.CourseId == attendanceDtos.CourseId))
        //    {
        //        _dbContext.Attendances.Remove(_dbContext.Attendances.Where(x => x.CourseId == attendanceDtos.CourseId && x.AttendeeId == userId).FirstOrDefault());
        //        _dbContext.SaveChanges();
        //        return Ok("Bạn đã hủy đăng kí tham gia");
        //    }



        //    var attendance = new Attendance
        //    {
        //        CourseId = attendanceDtos.CourseId,
        //        AttendeeId = userId
        //    };

        //    _dbContext.Attendances.Add(attendance);
        //    _dbContext.SaveChanges();

        //    return Ok("Successfully !");
        //}

        //[HttpDelete]
        //public IHttpActionResult DeleteAttendance(int id) {
        //    var userId = User.Identity.GetUserId();

        //    var attendance = _dbContext.Attendances
        //        .SingleOrDefault(a => a.AttendeeId == userId && a.CourseId == id);
        //    if (attendance == null)
        //        return NotFound();
        //    _dbContext.Attendances.Remove(attendance);
        //    _dbContext.SaveChanges();

        //    return Ok(id);

        //}

    }
}
