using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("checkin_tbl")]
    public class CheckIn
    {
		public int id { get; set; }
		public DateTime? checkindate { get; set; }
		public DateTime? startdate { get; set; }
		public DateTime? enddate { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public string userid { get; set; }
		public int guestid { get; set; }
        public Guest guest { get; set; }
        public int child { get; set; }
		public int man { get; set; }
		public int women { get; set; }
        public decimal pay { get; set; }
    }
}


