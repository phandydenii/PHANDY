using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class shiftDto
    {
        [Key]
        public int shiftid { get; set; }
        public string shiftname { get; set; }
        public string shiftstatus { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}