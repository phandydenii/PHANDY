using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class InvoiceDetailV
    {
        public int id { get; set; }
        public String invoiceno { get; set; }
        public DateTime invoicedate { get; set; }
        public string userid { get; set; }
        public decimal exchangerate { get; set; }
        public decimal grandtotal { get; set; }
        public decimal totalriel { get; set; }
        public decimal totaldollar { get; set; }
        public Boolean paid { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public string updateby { get; set; }
        public DateTime updatedate { get; set; }
        public string note { get; set; }
        public decimal owe { get; set; }
        public string owereassion { get; set; }
        public decimal totalreturnamount { get; set; }
        public decimal returnamount { get; set; }
        public string returndescription { get; set; }
        public string invoicestatus { get; set; }

        public int invoicedtailid { get; set; }
        public string roomno { get; set; }
        public decimal roomprice { get; set; }

        public DateTime? wpredate { get; set; }
        public decimal wprerecord { get; set; }
        public DateTime? wcurrentdate { get; set; }
        public decimal wcurrentrecord { get; set; }
        public decimal wprice { get; set; }

        public DateTime? ppredate { get; set; }
        public decimal pprerecord { get; set; }
        public DateTime? pcurrentdate { get; set; }
        public decimal pcurrentrecord { get; set; }
        public decimal pprice { get; set; }

        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }

        public int checkinid { get; set; }

    }
}