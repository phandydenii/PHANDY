using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class studylanguage
    {
        [Key]
        public int studylanguageid { get; set; }
        public string language { get; set; }
        public string languagestatus { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}