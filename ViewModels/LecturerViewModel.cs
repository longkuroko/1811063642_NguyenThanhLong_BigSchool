using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class LecturerViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public bool ImFollowing { get; set; }
        public bool MyFollower { get; set; }
        public int CourseCount
        {
            get; set;

        }
    }
}