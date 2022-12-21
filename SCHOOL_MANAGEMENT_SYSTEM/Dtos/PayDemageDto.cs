using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class PayDemageDto
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