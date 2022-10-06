using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("pricing_tbl")]
    public class Pricing
    {
		public int id { get; set; }
		public DateTime? date { get; set; }
		public int employeeid { get; set; }
		public Employee employee { get; set; }
		public string descrition { get; set; }
        public decimal pricing { get; set; }
        public string createby { get; set; }
		public DateTime? createdate { get; set; }
		public DateTime? updatedate { get; set; }
		public string updateby { get; set; }
		
		
	}
}