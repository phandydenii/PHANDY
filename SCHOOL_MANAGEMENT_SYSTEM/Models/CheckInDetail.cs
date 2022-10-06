using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("checkindetail_tbl")]
    public class CheckInDetail
    {
        public int id { get; set; }
        public int checkinid { get; set; }
        public CheckIn checkin { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
    }
}