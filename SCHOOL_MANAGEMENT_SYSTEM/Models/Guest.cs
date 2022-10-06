using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("guest_tbl")]
    public class Guest
    {
        public int id { get; set; }
        public string name { get; set; }
        public string namekh { get; set; }
        public string sex { get; set; }
        public DateTime? dob { get; set; }
        public string address { get; set; }
        public string nationality { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string ssn { get; set; }
        public string passport { get; set; }
        public string status { get; set; }
    }
}