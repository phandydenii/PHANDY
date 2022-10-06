using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("invoicedetail_tbl")]
    public class InvoiceDetail
    {
        [Key]
        public int id { get; set; }
        [Required]
        public int invoiceid { get; set; }
        public Invoice invoices { get; set; }

        [Required]
        public int locationid { get; set; }
        public Location locations { get; set; }
        [Required]
        public int productid { get; set; }
        public Product products { get; set; }
        [Required]
        public int employeeid { get; set; }
        public Employee employees { get; set; }
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
        public Boolean alreadymove { get; set; }
    }
}