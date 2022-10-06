using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class gradeDto
    {
        [Key]
        public int gradeid { get; set; }
        public string gradename { get; set; }
        public string gradestatus { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}