using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("position_tbl")]
    public class Position
    {
        public int id { get; set; }
        [Required]
        [StringLength(255)]
        public string positionname { get; set; }
        public string positionnamekh { get; set; }
        public Boolean status { get; set; }
    }
}