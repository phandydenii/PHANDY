using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class RegisterStudentV
    {
        [Key]
        public int id { get; set; }
        public int registerid { get; set; }
        public DateTime date { get; set; }
        public string userid { get; set; }
        public int studentid { get; set; }

        public int shiftid { get; set; }

        public int languageid { get; set; }

        public int periodid { get; set; }

        public int gradeid { get; set; }
        public string status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public string fullname { get; set; }
        public string shiftname { get; set; }
        public string language { get; set; }
        public string period { get; set; }
        public string gradename { get; set; }
        public string registerfile { get; set; }
    }
}