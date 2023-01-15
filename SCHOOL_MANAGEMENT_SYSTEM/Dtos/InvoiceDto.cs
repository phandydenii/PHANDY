using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class InvoiceDto
    {
        
        public int id { get; set; }
       
        public DateTime invoicedate { get; set; }
        public int guestid { get; set; }
        public Guest guest { get; set; }
        public int weusageid { get; set; }
        public WaterElectricUsage weusage { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public string userid { get; set; }
        public int exchangerateid { get; set; }
        public ExchangeRate exchangerate { get; set; }
        public decimal grandtotal { get; set; }
        public decimal totalriel { get; set; }
        public decimal totaldollar { get; set; }
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
        public string status { get; set; }

    }
}