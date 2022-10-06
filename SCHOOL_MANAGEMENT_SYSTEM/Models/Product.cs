using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("product_tbl")]
    public class Product
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string productname { get; set; }
        [Required]
        public int categoryid { get; set; }
        public Category Categorys { get; set; }
        [Required]
        public int showroomid { get; set; }
        public Showroom Showrooms { get; set; }
        public Decimal qtyonhand { get; set; }
        public string photo { get; set; }
        public Boolean status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}