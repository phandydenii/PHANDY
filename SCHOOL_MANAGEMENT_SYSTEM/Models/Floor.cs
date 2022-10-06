using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("floor_tbl")]
    public class Floor
    {
        public int id { get; set; }
        public string floor_no { get; set; }

        [Required]
        public int buildingid { get; set; }
        public Building building { get; set; }

        public string status { get; set; }
    }
}