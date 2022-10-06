using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("Salarys")]
    public class Salary
    {
        [Key]
        public int salaryId { get; set; }
        [Required]
        public int employeeid { get; set; }
        public Employee employee { get; set; }

        [Required]
        public Double salaryAmount { get; set; }
        public DateTime salaryDate { get; set; }
        public String salaryNote { get; set; }
        public DateTime createDate { get; set; }
        public String createBy { get; set; }



    }
}