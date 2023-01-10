using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("otherexpense_tbl")]
    public class OtherExpense
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        [Required]
        public int expensetypeid { get; set; }
        public ExpenseType ExpenseTypes { get; set; }
        public Decimal amount { get; set; }
        public string note { get; set; }

        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public string image { get; set; }
    }
}