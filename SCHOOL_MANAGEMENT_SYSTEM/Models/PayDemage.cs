using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("paydemage_tbl")]
    public class PayDemage
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int checkinid { get; set; }
        public CheckIn checkin { get; set; }
        public int itemid { get; set; }
        public Item item { get; set; }
        public string note { get; set; }
    }
}