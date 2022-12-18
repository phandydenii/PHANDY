using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("Salarys")]
    public class Salary
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int staffid { get; set; }
        public Staff staff { get; set; }
        public decimal salary { get; set; }
        public string note { get; set; }
        public DateTime createdate { get; set; }
        public string createby { get; set; }
    }
}