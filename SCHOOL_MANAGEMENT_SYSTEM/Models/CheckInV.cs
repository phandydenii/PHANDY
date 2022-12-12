using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class CheckInV
    {
        public int id { get; set; }
        public String userid { get; set; }
        public int roomid { get; set; }
        public string room_no { get; set; }
        public int roomtypeid { get; set; }
        public string roomtypename { get; set; }
        public int floorid { get; set; }
        public string floorno { get; set; }
        public string building { get; set; }
        public decimal servicecharge { get; set; }
        public decimal price { get; set; }
        public string roomkey { get; set; }
        public string roomstatus { get; set; }

        public DateTime? wolddate { get; set; }
        public DateTime? polddate { get; set; }
        public decimal woldrecord { get; set; }
        public decimal poldrecord { get; set; }

        public DateTime? wnewdate { get; set; }
        public DateTime? pnewdate { get; set; }
        public decimal wnewrecord { get; set; }
        public decimal pnewrecord { get; set; }
        public decimal pay { get; set; }
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

        public int child { get; set; }
        public int man { get; set; }
        public int women { get; set; }

        public DateTime? checkindate { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
    }
}