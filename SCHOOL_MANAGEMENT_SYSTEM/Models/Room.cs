using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("room_tbl")]
    public class Room
    {
		public int id { get; set; }
		public string room_no { get; set; }

		[Required]
		public int roomtypeid { get; set; }
		public RoomType roomType { get; set; }

		[Required]
		public int floorid { get; set; }
		public Floor floor { get; set; }

		public decimal servicecharge { get; set; }
		public decimal price { get; set; }		
		public string roomkey { get; set; }
		public string status { get; set; }
		public string note { get; set; }


    }
}