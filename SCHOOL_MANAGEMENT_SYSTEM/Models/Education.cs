using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class Education
    {
        [Key]
        public int educationId { get; set; }
        public int educationEmpid { get; set; }
        [ForeignKey("educationEmpid")]
        public Employees employee { get; set; }
        public String educationLevel { get; set; }
        public String skill { get; set; }
        public int fromYear { get; set; }
        public int toYear { get; set; }
        public DateTime createDate { get; set; }
        public String createBy { get; set; }

    }
}