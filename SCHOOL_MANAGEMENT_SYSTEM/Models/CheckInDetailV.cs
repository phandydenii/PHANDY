using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class CheckInDetailV
    {
		public int id { get; set; }
		public int checkinid { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}")]
        public DateTime? checkindate { get; set; }
		public DateTime? startdate { get; set; }
		public DateTime? enddate { get; set; }
		public int userid { get; set; }
        public int roomid { get; set; }
        public string roomno { get; set; }
        public string roomkey { get; set; }
        public string servicecharge { get; set; }
        public decimal roomprice { get; set; }

        public int roomtypeid { get; set; }
        public string roomtypename { get; set; }
        public string roomtypenamekh { get; set; }
        public int floorid { get; set; }
        public string floorno { get; set; }
        public int buildingid { get; set; }
        public string buildingname { get; set; }

        public int child { get; set; }
		public int man { get; set; }
		public int women { get; set; }

        public int guestid { get; set; }
        public string guestname { get; set; }
        public string guestnamekh { get; set; }
        public string sex { get; set; }
        public DateTime? dob { get; set; }
        public string address { get; set; }
        public string nationality { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string ssn { get; set; }
        public string passport { get; set; }
        public string status { get; set; }
    }
}