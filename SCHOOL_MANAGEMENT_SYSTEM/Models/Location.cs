using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("location_tbl")]
    public class Location
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string location { get; set; }
        public Boolean status { get; set; }
    }
}