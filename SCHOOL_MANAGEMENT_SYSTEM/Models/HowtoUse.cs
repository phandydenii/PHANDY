using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("howtouse_tbl")]
    public class HowtoUse
    {
        public int id { get; set; }
        public string note { get; set; }
        public string attachfile { get; set; }
        public int employeeid { get; set; }
        public Employee employee { get; set; }
    }
}