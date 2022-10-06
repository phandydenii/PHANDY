using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{

    [Table("invoice_move_tbl")]
    public class invoice_moveDto
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        public int invoiceid { get; set; }
        public string moveby { get; set; }
        public string note { get; set; }
    }
}