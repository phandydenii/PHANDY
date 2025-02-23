﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class WEPriceDto
    {
        public int id { get; set; }

        public DateTime date { get; set; }

        public decimal waterprice { get; set; }
        public decimal electricprice { get; set; }
        public bool status { get; set; }

        public bool IsDeleted { get; set; }
    }
}
