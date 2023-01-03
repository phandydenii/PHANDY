using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{

    public class RoomDetailDto
    {
        public int id { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public int itemid { get; set; }
        public Item item { get; set; }
        public decimal price { get; set; }
    }
}