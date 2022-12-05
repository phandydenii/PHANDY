using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class ElectricUsageDto
    {
        public int id { get; set; }
        public int checkinid { get; set; }
        public CheckIn checkin { get; set; }
        public DateTime? predate { get; set; }
        public decimal prerecord { get; set; }
        public DateTime? currentdate { get; set; }
        public decimal currentrecord { get; set; }
        public int price { get; set; }
    }
}