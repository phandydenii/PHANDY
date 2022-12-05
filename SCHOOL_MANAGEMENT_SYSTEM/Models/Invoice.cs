using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("invoice_tbl")]
    public class Invoice
    {
        [Key]
        public int id { get; set; }
        public String invoiceno { get; set; }
        public DateTime invoicedate { get; set; }
        public int guestid { get; set; }
        public Guest guest { get; set; }
        public int checkinid { get; set; }
        public CheckIn checkin { get; set; }
        public int electricid { get; set; }
        public ElectricUsage electric { get; set; }
        public int waterusageid { get; set; }
        public WaterUsage waterusage { get; set; }

        public string userid { get; set; }
        public int exchangerateid { get; set; }
        public ExchangeRate exchangerate { get; set; }
        public decimal grandtotal { get; set; }
        //public decimal vat { get; set; }
        public decimal totalriel { get; set; }
        public decimal totaldollar { get; set; }
        //public decimal discount { get; set; }
        public decimal totalother { get; set; }
        public decimal payriel { get; set; }
        public decimal paydollar { get; set; }
        public bool paid { get; set; }
        public bool printed { get; set; }
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
        public string status { get; set; }

    }
}
