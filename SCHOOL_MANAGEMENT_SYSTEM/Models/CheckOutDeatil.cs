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
        public string itemcharge { get; set; }
        public decimal itemprice { get; set; }
        public int electricid { get; set; }
        public ElectricUsage electric { get; set; }
        public int waterid { get; set; }
        public WaterUsage water { get; set; }

    }
}