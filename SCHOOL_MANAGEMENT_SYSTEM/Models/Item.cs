﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("item_tbl")]
    public class Item
    {
        public int id { get; set; }
        [Required]
        public string itemname { get; set; }
        public string itemnamekh { get; set; }
        public decimal price { get; set; }
        public string remark { get; set; }
        public string status { get; set; }
    }
}