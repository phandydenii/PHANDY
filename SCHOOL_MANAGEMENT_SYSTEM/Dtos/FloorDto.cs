using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class FloorDto
    {
        public int id { get; set; }
        public string floor_no { get; set; }

        [Required]
        public int buildingid { get; set; }
        public Building building { get; set; }
        public string status { get; set; }
    }
}