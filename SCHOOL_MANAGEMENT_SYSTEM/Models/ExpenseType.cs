using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("expensetype_tbl")]
    public class ExpenseType
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string typename { get; set; }

    }
}