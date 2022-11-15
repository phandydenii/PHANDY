using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class BookingV
    {
        public int id { get; set; }
        public string bookingno { get; set; }
        public DateTime? bookingdate { get; set; }
        public int userid { get; set; }
        public decimal total { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
        public DateTime? checkindate { get; set; }
        public DateTime? expirecheckindate { get; set; }
        public string note { get; set; }

        public int roomid { get; set; }
        public string room_no { get; set; }
        public int roomtypeid { get; set; }
        public string roomtypename { get; set; }
        public int exchangeid { get; set; }
        public decimal exchangerate { get; set; }
        public decimal servicecharge { get; set; }
        public decimal roomprice { get; set; }
        public string roomkey { get; set; }

        public int guestid { get; set; }
        public string name { get; set; }
        public string namekh { get; set; }
        public string sex { get; set; }
        public DateTime? dob { get; set; }
        public string address { get; set; }
        public string nationality { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string ssn { get; set; }
        public string passport { get; set; }
        public string gueststatus { get; set; }
    }
}