using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("waterpowerprice_tbl")]
    public class WaterPowerPrice
    {
        public int id { get; set; }

        public DateTime date { get; set; }

        public decimal waterprice { get; set; }
        public decimal powerprice { get; set; }
        public bool status { get; set; }

        public bool IsDeleted { get; set; }
    }
}