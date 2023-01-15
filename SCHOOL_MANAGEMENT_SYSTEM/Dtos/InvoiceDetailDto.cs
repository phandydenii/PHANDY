using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{

    public class InvoiceDetailDto
    {
        public int id { get; set; }
        public int invoiceid { get; set; }
        public Invoice invoice { get; set; }
        public int paydemageid { get; set; }
        public PayDemage paydemage { get; set; }
    }
}