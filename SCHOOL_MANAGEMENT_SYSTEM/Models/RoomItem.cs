using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("roomitem_tbl")]
    public class RoomItem
    {
        [Required]
        public int id { get; set; }

        [Required]
        public int roomid { get; set; }
        public Room room { get; set; }

        [Required]
        public int itemid { get; set; }
        public Item item { get; set; }
    }
}