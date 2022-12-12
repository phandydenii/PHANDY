using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("payslip_tbl")]
    public class PaySlip
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int staffid { get; set; }
        public Staff staff { get; set; }
        public decimal salary { get; set; }
        public decimal vat { get; set; }
        public decimal penanty { get; set; }
        public decimal bonus { get; set; }
        public decimal salaryamount { get; set; }
        public string note { get; set; }
    }
}