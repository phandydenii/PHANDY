using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class CheckOutDetailDto
    {
        public int id { get; set; }
        public int checkoutid { get; set; }
        public CheckOut checkout { get; set; }
        public int paydemageid { get; set; }
        public PayDemage paydemage { get; set; }
    }
}