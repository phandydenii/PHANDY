using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    [Table("showroom_tbl")]
    public class ShowroomDto
    {
        [Key]
        public int id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string owner { get; set; }
        public string phone { get; set; }
        public Boolean status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
    }
}