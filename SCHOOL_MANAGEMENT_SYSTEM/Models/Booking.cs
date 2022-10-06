using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("booking_tbl")]
    public class Booking
    {
		public int id { get; set; }
		public int booking_no { get; set; }
		public int userid { get; set; }
		public int guestid { get; set; }
		public decimal paydollar { get; set; }
		public string payriel { get; set; }
		public string note { get; set; }
		public DateTime? bookingdate { get; set; }
	}
}