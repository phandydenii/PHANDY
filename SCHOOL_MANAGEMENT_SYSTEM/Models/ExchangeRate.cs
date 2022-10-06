using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class ExchangeRate
    {
        [Key]
        public int rateid { get; set; }
        [Required]
        public DateTime date { get; set; }
        [Required]
        public decimal Rate { get; set; }
        public bool Status { get; set; }

        public bool IsDeleted { get; set; }
        public ExchangeRate()
        {

            IsDeleted = false;
        }
    }
}