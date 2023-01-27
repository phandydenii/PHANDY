using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("booking_tbl")]
    public class Booking
    {
		public int id { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? bookingdate { get; set; }
        public string userid { get; set; }
		public int guestid { get; set; }
        public Guest guest { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public int exchangeid { get; set; }
        public ExchangeRate exchange { get; set; }
        public decimal total { get; set; }
		public decimal paydollar { get; set; }
        public decimal payriel { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? updatedate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public string updateby { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? checkindate { get; set; }

        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime? expiredate { get; set; }

        public string note { get; set; }
		public string status { get; set; }
	}
}



