using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class CheckInDetailDto
    {
        public int id { get; set; }
        public int checkinid { get; set; }
        public CheckIn checkin { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
    }
}