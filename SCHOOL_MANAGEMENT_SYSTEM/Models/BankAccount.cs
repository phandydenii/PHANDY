using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations.Schema;


namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
	[Table("bankaccount_tbl")]

	public class BankAccount
    {
		public int id { get; set; }
		public DateTime? date {get;set;}
		public int customerid { get; set; }
        public Customer customer { get; set; }
        public string abaname { get; set; }
		public string abanumber { get; set; }
		public bool status { get; set; }
	}
}