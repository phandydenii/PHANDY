using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    [Table("invoice_tbl")]
    public class InvoiceDto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int invoiceno { get; set; }
        public DateTime date { get; set; }
        [Required]
        public int customerid { get; set; }
        public Customer Customers { get; set; }
        [Required]
        public int showroomid { get; set; }
        public Showroom Showrooms { get; set; }
        [Required]
        public int exchangeid { get; set; }
        public Decimal totalamount { get; set; }
        public Decimal totalcarprice { get; set; }
        public Decimal totalshipprice { get; set; }
        public Decimal alreadypaid { get; set; }
        public Boolean status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public Boolean paid { get; set; }
    }
}