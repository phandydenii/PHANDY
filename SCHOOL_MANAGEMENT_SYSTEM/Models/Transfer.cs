using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    [Table("transfer_tbl")]
    public class Transfer
    {
        [Key]
        public int id { get; set; }
        public DateTime date { get; set; }
        public Decimal amount { get; set; }
        public string note { get; set; }
        [Required]
        public int showroomid { get; set; }
        public Showroom Showrooms { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}