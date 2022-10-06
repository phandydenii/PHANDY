﻿using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class CustomerViewModel
    {
        public IEnumerable<Showroom> Showrooms { get; set; }
        public IEnumerable<Customer> Customers { get; set; }
    }
}