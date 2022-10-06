using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class courseDto
    {
        [Key]
        public int id { get; set; }
        public string coursecode { get; set; }
        public string coursename { get; set; }
        public string coursenamekh { get; set; }
        public decimal tutionfee { get; set; }
        public string status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}