using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("checkoutdetail_tbl")]
    public class CheckOutDeatil
    {
        public int id { get; set; }
        public int checkoutid { get; set; }
        public CheckOut checkout { get; set; }
        public int paydemageid { get; set; }
        public PayDemage paydemage { get; set; }

    }
}