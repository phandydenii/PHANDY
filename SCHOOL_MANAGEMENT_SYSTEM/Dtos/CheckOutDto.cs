using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class CheckOutDto
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int guestid { get; set; }
        public Guest guest { get; set; }
        public int roomid { get; set; }
        public int eletricusageid { get; set; }
        public ElectricUsage eletricusage { get; set; }
        public int waterusageid { get; set; }
        public WEPrice waterusage { get; set; }
        public Room room { get; set; }
        public int exchangeid { get; set; }
        public ExchangeRate exchange { get; set; }
        public string userid { get; set; }
        
        public decimal total { get; set; }
        public decimal paybefor { get; set; }
        public decimal returnamount { get; set; }
        public decimal totalpayment { get; set; }
        public string description { get; set; }
    }
}