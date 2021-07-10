using BigSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class LecutureFollowingViewModel
    {
        public IEnumerable<Following> LectureFollowing { get; set; }
    }
}