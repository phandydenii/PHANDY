﻿using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class CheckInDto
    {
        public int id { get; set; }
        public DateTime? checkindate { get; set; }
        public DateTime? startdate { get; set; }
        public DateTime? enddate { get; set; }
        public int roomid { get; set; }
        public Room room { get; set; }
        public String userid { get; set; }
        public int guestid { get; set; }
        public Guest guest { get; set; }
        public int child { get; set; }
        public int man { get; set; }
        public int women { get; set; }
        public decimal payforroom { get; set; }
        public decimal prepaid { get; set; }
        public decimal paydollar { get; set; }
        public decimal payriel { get; set; }
        public bool active { get; set; }
    }
}