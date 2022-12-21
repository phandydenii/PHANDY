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
        public int paydemageid { get; set; }
        public PayDemage paydemage { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
    }
}