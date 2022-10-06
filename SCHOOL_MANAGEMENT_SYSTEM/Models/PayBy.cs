using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("payby_tbl")]
    public class PayBy
    {
		public int id { get; set; }
		public DateTime? paybydate { get; set; }
		public string paymentmethod { get; set; }
		public string screenshot { get; set; }
		public string note { get; set; }
		public int userid { get; set; }
	}
}