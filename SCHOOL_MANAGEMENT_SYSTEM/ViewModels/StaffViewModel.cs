using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class StaffViewModel
    {
        public IEnumerable<Position> Positions { get; set; }
        public IEnumerable<Staff> StaffList { get; set; }
    }
}