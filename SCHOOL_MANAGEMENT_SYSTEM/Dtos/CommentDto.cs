using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{

    [Table("comment_tbl")]
    public class CommentDto
    {
        public int id { get; set; }
        public int invoiceid { get; set; }
        public string comment { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public Boolean status { get; set; }

    }
}