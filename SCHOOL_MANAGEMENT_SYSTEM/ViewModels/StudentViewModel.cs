using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class StudentViewModel
    {
        public List<Branch> Branchs { get; internal set; }
        public IEnumerable<grade> Grades { get; set; }
        public IEnumerable<shifts> Shifts { get; set; }
    }
}