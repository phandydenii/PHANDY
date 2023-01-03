using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("waterelectricusage_tbl")]
    public class WaterElectricUsage
    {
        public int id { get; set; }
        public int  checkinid { get; set; }
        public CheckIn checkin { get; set; }
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
