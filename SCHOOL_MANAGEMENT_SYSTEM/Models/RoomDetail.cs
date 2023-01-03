using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("roomdetail_tbl")]
    public class RoomDetail
    {
        public int id { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public int itemid { get; set; }
        public Item item { get; set; }
        public decimal price { get; set; }
    }
}