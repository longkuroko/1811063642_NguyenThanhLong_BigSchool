using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class CoursesViewModel
    {
        //cham bai
        public string dataSearch { get; set; }
        public IEnumerable<Course> UpcommingCourses { get; set; }
        public bool ShowAction { get; set; }

        private readonly ApplicationDbContext _dbContext;

        public CoursesViewModel()
        {
            _dbContext = new ApplicationDbContext();
        }

        public bool CheckFollowing(string followeeId, string userId)
        {
            var listFollowing = _dbContext.Followings
                .Where(c => c.FollowerId == userId)
                .ToList();

            foreach (var i in listFollowing)
            {
                if (i.FolloweeId.Equals(followeeId))
                {
                    return true;
                }
            }
            return false;
        }

        public bool CheckAttending(int courseId, string userId)
        {
            var listAttending = _dbContext.Attendances
                .Where(c => c.AttendeeId == userId)
                .ToList();
            foreach (var i in listAttending)
            {
                if (i.CourseId == courseId)
                {
                    return true;
                }
            }
            return false;
        }
    }
}