using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class EmployeesViewModel
    {
        public IEnumerable<Branch> Branchs { get; set; }

        public IEnumerable<Department> Departments { get; set; }
    }
}