using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class WaterElectricUsageDto
    {
        public int id { get; set; }
        public int guestid { get; set; }
        public Guest guest { get; set; }
        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public decimal wstartrecord { get; set; }
        public decimal wendrecord { get; set; }
        public decimal estartrecord { get; set; }
        public decimal eendrecord { get; set; }
        public int wepriceid { get; set; }
        public WEPrice weprice { get; set; }
    }
}