using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("emergency_tbl")]
    public class emergency
    {
        [Key]
        public int emerid { get; set; }
        
        public int emerstudentid { get; set; }
        [ForeignKey("emerstudentid")]
        public student student { get; set; }
        [Required]
        public string name1 { get; set; }
        [Required]
        public string contactnumber1 { get; set; }
        public string relationship1 { get; set; }
        public string name2 { get; set; }
        public string contactnumber2 { get; set; }
        public string relationship2 { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}