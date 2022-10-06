using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class student
    {
        [Key]
        public int id { get; set; }
        public int studentid { get; set; }
        [Required]
        public int studentbranchid { get; set; }
        [ForeignKey("studentbranchid")]
        public Branch Branch { get; set; }
        public string surname { get; set; }
        public string firstname { get; set; }
        public string fullname { get; set; }
        public string fullnamekh { get; set; }
        public string studentgender { get; set; }
        public DateTime studentdob { get; set; }
        public string studentpob { get; set; }
        public string nationality { get; set; }
        public string nativelanguage { get; set; }
        public string otherspoken { get; set; }
        public string schoolyear { get; set; }
        public string studentphoto { get; set; }

        [Required]
        public int studentshiftid { get; set; }
        [ForeignKey("studentshiftid")]
        public shifts Shiftes { get; set; }

        [Required]
        public int studentgradeid { get; set; }
        [ForeignKey("studentgradeid")]
        public grade Grade { get; set; }
        public string studentstatus { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }

       

    }
}