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
        [Required]
        public int id { get; set; }

        [Required]
        public int roomid { get; set; }
        public Room room { get; set; }

        [Required]
        public int itemid { get; set; }
        public Item item { get; set; }
        public decimal price { get; set; }
        public string note { get; set; }
    }
}