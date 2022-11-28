using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("waterusage_tbl")]
    public class WaterUsage
    {
        public int id { get; set; }
        public int invoiceid { get; set; }
        public Invoice Invoice { get; set; }
        public DateTime? predate { get; set; }
        public decimal prerecord { get; set; }
        public DateTime? currentdate { get; set; }
        public decimal currentrecord { get; set; }
        public int price { get; set; }
    }
}