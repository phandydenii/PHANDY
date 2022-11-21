using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class InvoiceCheckInV
    {
        public DateTime invoicedate { get; set; }
        public bool paid { get; set; }
        public bool printed { get; set; }
        public int checkinid { get; set; }
        public DateTime? checkindate { get; set; }
        public string roomno { get; set; }
        public string guestname { get; set; }
        public string guestnamekh { get; set; }
        public string floorno { get; set; }
        public string building { get; set; }
    }
}