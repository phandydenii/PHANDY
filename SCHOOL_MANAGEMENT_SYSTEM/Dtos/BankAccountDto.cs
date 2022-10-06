﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class BankAccountDto
    {
        public int id { get; set; }
        public DateTime? date { get; set; }
        public int customerid { get; set; }
        public Customer customer { get; set; }
        public string abaname { get; set; }
        public string abanumber { get; set; }
        public bool status { get; set; }
    }
}