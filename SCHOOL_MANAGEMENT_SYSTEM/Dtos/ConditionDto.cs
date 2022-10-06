using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class ConditionDto
    {
		public int id { get; set; }
		public DateTime? date {get;set;}
		public string conditionnote { get; set; }
		public string createby { get; set; }
		public DateTime? updatedate { get; set; }
		public string updateby { get; set; }
		public DateTime? createdate { get; set; }
		public int employeeid { get; set; }
        public Employee employee { get; set; }
    }
}