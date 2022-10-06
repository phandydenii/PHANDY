using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class RoomDto
    {
		public int id { get; set; }
		public string room_no { get; set; }

		[Required]
		public int roomtypeid { get; set; }
		public RoomType roomType { get; set; }

		[Required]
		public int floorid { get; set; }
		public Floor floor { get; set; }

		public string servicecharge { get; set; }
		public decimal price { get; set; }
		public string roomkey { get; set; }
		public string status { get; set; }
    }
}