using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    [Table("category_tbl")]
    public class CategoryDto
    {
        [Key]
        public int id { get; set; }
        [Required]
        public string categoryname { get; set; }
        public Boolean status { get; set; }
    }
}