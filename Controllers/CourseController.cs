using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class CourseController : Controller
    {

        private readonly Models.ApplicationDbContext _dbContext;
        public CourseController()
        {
            _dbContext = new Models.ApplicationDbContext();
        }

        public ActionResult Details(int id)
        {
            var viewModel = _dbContext.Courses.Include(x => x.Category).Include(x => x.Lecturer).Where(x => x.Id == id).FirstOrDefault();
            if (viewModel == null)
                return HttpNotFound();
            return View(viewModel);
        }




        public ActionResult Create()
        {
            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList()
              
            };
            return View(viewModel);


        }



        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View(viewModel);
            }
            var course = new Course
            {
                LecturerId = User.Identity.GetUserId(),
                DateTime = viewModel.GetDateTime(),
                CategoryId = viewModel.Category,
                Place = viewModel.Place
            };
            _dbContext.Courses.Add(course);
            _dbContext.SaveChanges();

            return RedirectToAction("Index","Home");
        }


        [Authorize]
        public ActionResult Attending()
        {
            var userId = User.Identity.GetUserId();

            var courses = _dbContext.Attendances
                .Where(a => a.AttendeeId == userId)
                .Select(a => a.Course)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();
            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };


            return View(viewModel);
        }
        //public ActionResult Attending()
        //{
        //    var userId = User.Identity.GetUserId();
        //    var courses = _dbContext.Attendances
        //        .Where(a => a.AttendeeId == userId)
        //        .Select(a => a.Course).Where(x => !x.IsCanceled && x.DateTime > DateTime.Now).OrderBy(x => x.DateTime)
        //        .Include(l => l.Lecturer)
        //        .Include(l => l.Category)
        //        .ToList();

        //    foreach (var item in courses)
        //    {
        //        item.isAttended = true;
        //    }

        //    var viewModel = new CoursesViewModel
        //    {
        //        UpcommingCourses = courses,
        //        ShowAction = User.Identity.IsAuthenticated
        //    };

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
        //    return View(viewModel);
        //}
        [Authorize]
        public  ActionResult UpcommingCourse()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses
                .Where(c => c.LecturerId == userId && c.DateTime > DateTime.Now)
                .Include(l => l.Lecturer)
                .Include(l => l.Category)
                .ToList();

            return View(courses);
        }

        [Authorize]
        public ActionResult Mine()
        {
            var userId = User.Identity.GetUserId();
            var courses = _dbContext.Courses.Include(x => x.Lecturer).Include(x => x.Category).Where(x => x.LecturerId == userId).OrderBy(x => x.DateTime).ToList();

            var viewModel = new CoursesViewModel
            {
                UpcommingCourses = courses,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult Edit(int id)
        {
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Single(c => c.Id == id && c.LecturerId == userId);

            var viewModel = new CourseViewModel
            {
                Categories = _dbContext.Categories.ToList(),
                Date = course.DateTime.ToString("dd/M/yyyy"),
                Time = course.DateTime.ToString("HH:mm"),
                Category = course.CategoryId,
                Place = course.Place
             
            };
            return View( viewModel);
        }

        //update
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update(CourseViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Categories = _dbContext.Categories.ToList();
                return View(viewModel);
            }
            var userId = User.Identity.GetUserId();
            var course = _dbContext.Courses.Find(viewModel.Id);
           
            course.Place = viewModel.Place;
            course.DateTime = viewModel.GetDateTime();
            course.CategoryId = viewModel.Category;

            _dbContext.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

      


    }
}