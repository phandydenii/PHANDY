using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class appropriateDto
    {
        [Key]
        public int appid { get; set; }
        [Required]
        public int appstudentid { get; set; }
        [ForeignKey("appstudentid")]
        public student students { get; set; }
        public string livewith { get; set; }
        public string speakhome { get; set; }
        public bool speakenglish { get; set; }
        public bool speakchinese { get; set; }
        public bool usetostudy { get; set; }
        public string usetostudynote { get; set; }
        public bool toilattrained { get; set; }
        public bool illness { get; set; }
        public string illnessnote { get; set; }
        public bool allergies { get; set; }
        public string allergiesnote { get; set; }
        public bool fears { get; set; }
        public string fearsnote { get; set; }
        public string sufferinfor { get; set; }
        public string othernote { get; set; }
        public bool playsport { get; set; }
        public string playsportnote { get; set; }
        public bool allowphoto { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }

    }
}