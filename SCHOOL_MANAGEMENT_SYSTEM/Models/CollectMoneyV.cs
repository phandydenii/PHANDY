using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{

    [Table("collectmoney_tbl")]
    public class CollectMoneyV
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        [Required]
        public int employeeid { get; set; }
        public Employee Employees { get; set; }
        [Required]
        public Decimal amount { get; set; }
        public Decimal deliveryin { get; set; }
        public Decimal deliveryout { get; set; }
        public Decimal bonus { get; set; }
        public Boolean status { get; set; }
        public String createby { get; set; }
        public DateTime createdate { get; set; }
        public string namekh { get; set; }
    }
}