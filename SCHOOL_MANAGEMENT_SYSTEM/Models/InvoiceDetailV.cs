using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class InvoiceDetailV
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int invoiceid { get; set; }
        public Invoice Invoices { get; set; }
        [Required]
        public int locationid { get; set; }
        public Location Locations { get; set; }
        [Required]
        public int productid { get; set; }
        public Product Products { get; set; }
        [Required]
        public int employeeid { get; set; }
        public Employee Employees { get; set; }
        public string deliverytype { get; set; }
        public string receiverphone { get; set; }
        public Boolean paidtype { get; set; }
        public Decimal price { get; set; }
        public Decimal pricekh { get; set; }
        public Decimal carprice { get; set; }
        public Decimal shipprice { get; set; }
        public string status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public string updateby { get; set; }
        public DateTime updatedate { get; set; }
        public string location { get; set; }
        public string productname { get; set; }
        public string employeename { get; set; }
        public Boolean alreadymove { get; set; }

    }
}