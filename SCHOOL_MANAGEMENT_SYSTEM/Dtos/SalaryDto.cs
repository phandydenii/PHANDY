using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    [Table("Salarys")]
    public class SalaryDto
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