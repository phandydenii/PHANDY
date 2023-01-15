using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class PaySlipDto
    {
        public int id { get; set; }
        public DateTime date { get; set; }
        public int staffid { get; set; }
        public Staff staff { get; set; }
        public decimal salary { get; set; }
        public decimal vat { get; set; }
        public decimal penanty { get; set; }
        public decimal bonus { get; set; }
        public decimal totalsalary { get; set; }
        public string note { get; set; }
    }
}