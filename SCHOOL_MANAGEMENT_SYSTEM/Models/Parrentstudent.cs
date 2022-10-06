using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class Parrentstudent
    {
        
        [Key]
        public int parrentId { get; set; }
        [Required]
        public int parrentStuId { get; set; }
        [ForeignKey("parrentStuId")]
        public student students { get; set; }
        [StringLength(100)]
        public String fatherName { get; set; }
        [StringLength(100)]
        public String fatherJob { get; set; }
        public String motherName { get; set; }
        [StringLength(255)]
        public String motherJob { get; set; }
        public String parrentPhone { get; set; }
        [StringLength(255)]
        public String parrentAddress { get; set; }
        [StringLength(255)]
        public String contactPerson { get; set; }
        public String contactPhone { get; set; }
        public DateTime createDate { get; set; }
        public String createBy { get; set; }
    }
}