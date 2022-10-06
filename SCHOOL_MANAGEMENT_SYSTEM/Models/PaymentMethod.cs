using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("paymentmethod_tbl")]
    public class PaymentMethod
    {
        public int id { get; set; }
        public string methodname { get; set; }
        public string accountno { get; set; }
        public string accountname { get; set; }
        public bool status { get; set; }
    }
}