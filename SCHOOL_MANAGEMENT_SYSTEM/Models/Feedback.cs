using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("feedback_tbl")]

	public class Feedback
    {
		public int id { get; set; }
		public DateTime date {get;set;}
		public int customerid { get; set; }
		public Customer customer { get; set; }
		public string comment { get; set; }
		public bool status { get; set; }
		public string image {get;set;}
    }
}