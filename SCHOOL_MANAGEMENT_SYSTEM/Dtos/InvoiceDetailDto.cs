using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{

    public class InvoiceDetailDto
    {
        public int id { get; set; }

        public int invoiceid { get; set; }
        public Invoice invoice { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public int waterusageid { get; set; }
        public WaterUsage waterusage { get; set; }
        public int powerusageid { get; set; }
        public PowerUsage powerusage { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
    }
}