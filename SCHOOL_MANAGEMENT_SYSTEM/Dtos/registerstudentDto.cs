using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class registerstudentDto
    {
        [Key]
        public int id { get; set; }
        public int registerid { get; set; }
        public DateTime date { get; set; }
        public string userid { get; set; }

        public int studentid { get; set; }
        [ForeignKey("studentid")]
        public student students { get; set; }

        public int shiftid { get; set; }
        [ForeignKey("shiftid")]
        public shifts shifts { get; set; }

        public int languageid { get; set; }
        [ForeignKey("languageid")]
        public studylanguage studylanguages { get; set; }

        public int periodid { get; set; }
        [ForeignKey("periodid")]
        public studyperiod studyperiods { get; set; }

        public int gradeid { get; set; }
        [ForeignKey("gradeid")]
        public grade grades { get; set; }

        public string registerfile { get; set; }
        public string status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }

        
    }
}