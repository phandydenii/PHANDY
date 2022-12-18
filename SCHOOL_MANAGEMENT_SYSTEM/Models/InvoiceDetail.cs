using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("invoicedetail_tbl")]
    public class InvoiceDetail
    {
        public int id { get; set; }
        public int invoiceid { get; set; }
        public Invoice invoice { get; set; }
        public string itemname { get; set; }
        public decimal price { get; set; }
        public int waterusageid { get; set; }
        public WaterUsage waterusage { get; set; }
        public int electricusageid { get; set; }
        public ElectricUsage electric { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
        public string note { get; set; }
    }
}