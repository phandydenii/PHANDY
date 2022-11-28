using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class NewInvoiceV
    {
        public int checkinid { get; set; }
        public DateTime checkindate { get; set; }
        public string room_no { get; set; }
        public string roomtypename { get; set; }
        public decimal servicecharge { get; set; }
        public decimal price { get; set; }
        public string floor_no { get; set; }
        public string buildingname { get; set; }
        public int guestid { get; set; }
        public string name { get; set; }
        public string namekh { get; set; }
        public string sex { get; set; }
        public string dob { get; set; }
        public string address { get; set; }
        public string nationality { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string ssn { get; set; }
        public string passport { get; set; }
        public string status { get; set; }

        public DateTime startdate { get; set; }
        public DateTime enddate { get; set; }
        public bool printed { get; set; }
        public bool paid { get; set; }
        public string NewInvoice { get; set; }

        public DateTime wolddate { get; set; }
        public decimal woldrecord { get; set; }
        public DateTime polddate { get; set; }
        public decimal poldrecord { get; set; }

    }
}



