using BigSchool.Models;
using BigSchool.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BigSchool.Controllers
{
    public class LecturersController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public LecturersController()
        {
            _dbContext = new ApplicationDbContext();
        }
        // GET: Lecturers
        public ActionResult Index()
        {
            var lectures = new List<LecturerViewModel>();
            var userId = User.Identity.GetUserId();
            foreach (var item in _dbContext.Users.Where(x => x.Id != userId).ToList())
            {
                var imFollowing = _dbContext.Followings.Any(x => x.FollowerId == userId && x.FolloweeId == item.Id);
                var myFollower = _dbContext.Followings.Any(x => x.FollowerId == item.Id && x.FolloweeId == userId);
                var courseCount = _dbContext.Courses.Count(x => x.LecturerId == item.Id);

                lectures.Add(new LecturerViewModel
                {
                    Id = item.Id,
                    Name = item.Name,
                    ImFollowing = imFollowing,
                    MyFollower = myFollower,
                    CourseCount = courseCount
                });
            }
            var viewModel = new LecturersViewModel
            {
                Lecturers = lectures,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
        public ActionResult Following()
        {
            var lectures = new List<LecturerViewModel>();
            var userId = User.Identity.GetUserId();
            foreach (var item in _dbContext.Users.Where(x => x.Id != userId).ToList())
            {
                var imFollowing = _dbContext.Followings.Any(x => x.FollowerId == userId && x.FolloweeId == item.Id);
                if (imFollowing)
                {
                    var myFollower = _dbContext.Followings.Any(x => x.FollowerId == item.Id && x.FolloweeId == userId);
                    var courseCount = _dbContext.Courses.Count(x => x.LecturerId == item.Id);
                    lectures.Add(new LecturerViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ImFollowing = imFollowing,
                        MyFollower = myFollower,
                        CourseCount = courseCount
                    });
                }
            }
            var viewModel = new LecturersViewModel
            {
                Lecturers = lectures,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }

        public ActionResult Follower()
        {
            var lectures = new List<LecturerViewModel>();
            var userId = User.Identity.GetUserId();
            foreach (var item in _dbContext.Users.Where(x => x.Id != userId).ToList())
            {
                var myFollower = _dbContext.Followings.Any(x => x.FollowerId == item.Id && x.FolloweeId == userId);
                if (myFollower)
                {
                    var imFollowing = _dbContext.Followings.Any(x => x.FollowerId == userId && x.FolloweeId == item.Id);
                    var courseCount = _dbContext.Courses.Count(x => x.LecturerId == item.Id);
                    lectures.Add(new LecturerViewModel
                    {
                        Id = item.Id,
                        Name = item.Name,
                        ImFollowing = imFollowing,
                        MyFollower = myFollower,
                        CourseCount = courseCount
                    });
                }
            }
            var viewModel = new LecturersViewModel
            {
                Lecturers = lectures,
                ShowAction = User.Identity.IsAuthenticated
            };
            return View(viewModel);
        }
    }
}
