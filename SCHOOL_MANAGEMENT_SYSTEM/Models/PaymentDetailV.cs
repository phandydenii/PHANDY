using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class PaymentDetailV
    {
        [Key]
        public int id { get; set; }
        public int paymentid { get; set; }
        public int courseid { get; set; }
        public string coursename { get; set; }
        public string coursenamekh { get; set; }
        public int qty { get; set; }
        public decimal turtionfee { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
       
    }
}