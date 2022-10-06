using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class RoomV
    {
		public int id { get; set; }
		public string room_no { get; set; }
		public int roomtypeid { get; set; }
        public string roomtypename { get; set; }
        public int floorid { get; set; }
        public string floorno { get; set; }
        public string servicecharge { get; set; }
		public decimal price { get; set; }
		public string roomkey { get; set; }
		public string status { get; set; }
	}
}