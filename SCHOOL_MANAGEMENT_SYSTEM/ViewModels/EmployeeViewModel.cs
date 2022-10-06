using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class EmployeeViewModel
    {
        public IEnumerable<Position> Position { get; set; }

        public IEnumerable<Department> Department { get; set; }

        public IEnumerable<Showroom> Showroom { get; set; }

        public IEnumerable<Bonus> Bonus { get; set; }

    }
}