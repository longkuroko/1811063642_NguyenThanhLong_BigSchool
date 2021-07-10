﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace BigSchool.Models
{
    public class Notification
    {
        public int Id { get; set; }

        public NotificationType NotificationType { get; set; }

        public DateTime DateTime { get; set; }

        public DateTime OriginalDateTime { get; set; }

        public string OriginalPlace { get; set; }

        public int CourseId { get; set; }
        public Course Course { get; set; }
    }
}