﻿using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;

namespace BigSchool.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _dbContext;
        public HomeController()
        {
            _dbContext = new ApplicationDbContext();
        }
        public ActionResult Index()
        {
            var upcommingCourses = _dbContext.Courses
                .Include(c => c.Lecturer)
                .Include(c => c.Category)
                .Where(c => c.DateTime > DateTime.Now);
            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = upcommingCourses,
                ShowAction = User.Identity.IsAuthenticated
            };

            //if (User.Identity.IsAuthenticated)
            //{
            //    var userId = User.Identity.GetUserId();

            //    foreach (var item in viewModel.UpcommingCourses)
            //    {
            //        if (_dbContext.Attendances.Any(x => x.CourseId == item.Id && x.AttendeeId == userId))
            //        {
            //            item.isAttended = true;
            //        }
            //        if (_dbContext.Followings.Any(x => x.FollowerId == userId && x.FolloweeId == item.LecturerId))
            //        {
            //            item.isFollowed = true;
            //        }
            //    }
            //}
            return View(viewModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}