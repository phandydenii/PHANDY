using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class InvoiceV
    {
        public int id { get; set; }
        public int checkinid { get; set; }
        public DateTime invoicedate { get; set; }
        public int guestid { get; set; }
        public string guestname { get; set; }
        public string guestnamekh { get; set; }
        public string userid { get; set; }
        public decimal exchangerate { get; set; }
        public decimal grandtotal { get; set; }
        public decimal totalriel { get; set; }
        public decimal totaldollar { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
        public bool paid { get; set; }
        public bool isprint { get; set; }
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

        public int weid { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public decimal wstartrecord { get; set; }
        public decimal estartrecord { get; set; }
        public decimal wtotal { get; set; }
        public decimal waterusage { get; set; }
        public decimal wendrecord { get; set; }
        public decimal eendrecord { get; set; }
        public decimal etotal { get; set; }
        public decimal electricusage { get; set; }


        public DateTime? checkindate { get; set; }
        public string roomno { get; set; }
        public decimal roomprice { get; set; }
        public string roomtypename { get; set; }
        public string floorno { get; set; }
        public string building { get; set; }
        public decimal servicecharge { get; set; }
        public string roomkey { get; set; }
        public string roomstatus { get; set; }  
    }
}