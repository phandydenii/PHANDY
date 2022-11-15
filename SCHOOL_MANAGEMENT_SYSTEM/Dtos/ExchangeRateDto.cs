using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    public class ExchangeRateDto
    {
       
        public int id { get; set; }
      
        public DateTime date { get; set; }
    
        public decimal Rate { get; set; }
        public bool Status { get; set; }
        public bool IsDeleted { get; set; }

    }
}