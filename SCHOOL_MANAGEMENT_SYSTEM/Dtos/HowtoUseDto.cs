using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class HowtoUseDto
    {
        public int id { get; set; }
        public string note { get; set; }
        public string attachfile { get; set; }
        public int employeeid { get; set; }
        public Employee employee { get; set; }
    }
}