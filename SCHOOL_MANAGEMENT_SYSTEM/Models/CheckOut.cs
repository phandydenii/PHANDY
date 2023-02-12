using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("checkout_tbl")]
    public class CheckOut
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int guestid { get; set; }
        public Guest guest { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public int weusageid { get; set; }
        public WaterElectricUsage weusage { get; set; }

        public int exchangeid { get; set; }
        public ExchangeRate exchange { get; set; }
        public string userid { get; set; }
        public decimal totalroomprice { get; set; }
        public decimal total { get; set; }
        public decimal paybefor { get; set; }
        public decimal returnamount { get; set; }
        public decimal totalpayment { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
        public string description { get; set; }
    }
}