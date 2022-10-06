using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{

    [Table("expensetype_tbl")]
    public class ExpenseTypeDto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string typename { get; set; }

    }
}