using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("staff_tbl")]
    public class Staff
    {
        
        public int id { get; set; }

        [Required]
        public int positionid { get; set; }
        public Position position { get; set; }

        [Required]
        [Display(Name = "Name")]
        public String name { get; set; }
        [Required]
        [Display(Name = "Name in Khmer")]
        public String namekh { get; set; }
        public String sex { get; set; }
        public String phone { get; set; }
        public DateTime dob { get; set; }
        public String address { get; set; }
        public String email { get; set; }
        public String identityno { get; set; }
        public String photo { get; set; }
        public Boolean status { get; set; }
        public String createby { get; set; }
        public DateTime createdate { get; set; }
    }
}