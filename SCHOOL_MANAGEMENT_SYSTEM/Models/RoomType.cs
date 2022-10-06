using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("roomtype_tbl")]
    public class RoomType
    {
        public int id { get; set; }
        public string roomtypename { get; set; }
        public string roomtypenamekh { get; set; }
        public string note { get; set; }
    }
}