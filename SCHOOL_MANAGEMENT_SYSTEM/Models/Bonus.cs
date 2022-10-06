using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("bunus_weeken_borrow_tbl")]
    public class Bonus
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        public string type { get; set; }
        public int employeeid { get; set; }
        public Employee Emloyees { get; set; }
        public Decimal amount { get; set; }
        public string note { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}