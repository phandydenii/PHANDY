using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class FloorV
    {
        public int id { get; set; }
        public string floor_no { get; set; }
        public int buildingid { get; set; }
        public string buildingname { get; set; }
        public string buildingnamekh { get; set; }
        public string status { get; set; }
    }
}