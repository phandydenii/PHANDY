using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("ExchangeRates")]
    public class ExchangeRate
    {

        
        public int id { get; set; }
       
        public DateTime date { get; set; }
       
        public decimal Rate { get; set; }
        public bool Status { get; set; }

        public bool IsDeleted { get; set; }

    }
}