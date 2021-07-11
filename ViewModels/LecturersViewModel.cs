using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BigSchool.ViewModels
{
    public class LecturersViewModel
    {
        public IEnumerable<LecturerViewModel> Lecturers { get; set; }
        public bool ShowAction { get; set; }
    }
}