using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class RoomDetailV
    {
        public int id { get; set; }
        public int roomid { get; set; }
        public string room_no { get; set; }
        public int roomtypeid { get; set; }
        public string roomtypename { get; set; }
        public int floorid { get; set; }
        public string floorno { get; set; }
        public int buildingid { get; set; }
        public string buildingname { get; set; }
        public string servicecharge { get; set; }
        public decimal price { get; set; }
        public string roomkey { get; set; }
        public string status { get; set; }

        public int itemid { get; set; }
        public string itemname { get; set; }
        public string itemnamekh { get; set; }
    }
}