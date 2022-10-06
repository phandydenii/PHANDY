using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("building_tbl")]
    public class Building
    {
        public int id { get; set; }
        public string buildingname { get; set; }
        public string buildingnamekh { get; set; }
    }
}