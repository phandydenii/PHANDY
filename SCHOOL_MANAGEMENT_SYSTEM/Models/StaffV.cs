using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class StaffV
    {
        public int id { get; set; }
        public String positionname { get; set; }
        public String positionnamekh { get; set; }
        public String name { get; set; }
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