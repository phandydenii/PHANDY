using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class Experience
    {
        public int Id { get; set; }
        public int employee_id { get; set; }
        [ForeignKey("employee_id")]
        public Employees employee { get; set; }
        public String work_location { get; set; }
        public String position { get; set; }
        public DateTime from_year { get; set; }
        public DateTime to_year { get; set; }
        public DateTime create_date { get; set; }
        public String create_by { get; set; }
    }
}