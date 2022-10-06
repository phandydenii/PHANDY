using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class Employees
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int BranchId { get; set; }
 
        public Branch Branch { get; set; }

        [Required]
        public int DepartmentId { get; set; }

        public Department Department { get; set; }


        public String marital_Status { get; set; }
        [Required]
        [Display(Name ="Employee Name")]
        public String name { get; set; }
        [Required]
        [Display(Name = "Name in Khmer")]
        public String name_kh { get; set; }
        public String gender { get; set; }
        public String phone { get; set; }
        public String email { get; set; }

        public String password { get; set; }
        public String emp_address { get; set; }
        public String img { get; set; }
        public DateTime dob { get; set; }
        public String pob { get; set; }
        public Boolean is_active { get; set; }
        public String create_by { get; set; }
        public DateTime create_date { get; set; }


    }

}