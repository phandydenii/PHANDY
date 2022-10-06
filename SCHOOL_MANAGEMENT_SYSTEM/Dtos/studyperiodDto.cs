using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class studyperiodDto
    {
        [Key]
        public int studyperiodid { get; set; }
        public string period { get; set; }
        public decimal adminfee { get; set; }
        public string periodstatus { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}