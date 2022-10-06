using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("customer_tbl")]
    public class Customer
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string name { get; set; }
        public string sex { get; set; }
        public string phone { get; set; }
        public string address { get; set; }
        public string identityno { get; set; }
        public string photo { get; set; }
        public string customertype { get; set; }
        [Required]
        public int showroomid { get; set; }
        public Showroom Showroom { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public Boolean status { get; set; }
    }
}