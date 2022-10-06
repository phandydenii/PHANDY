using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Dtos
{
    [Table("employee_tbl")]
    public class EmployeeDto
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int showroomid { get; set; }

        public Showroom Showroom { get; set; }


        [Required]
        public int departmentid { get; set; }

        public Department Department { get; set; }


        public int positionid { get; set; }
        [Required]
        public Position Position { get; set; }

        [Required]
        [Display(Name = "Employee Name")]
        public String name { get; set; }
        [Required]
        [Display(Name = "Name in Khmer")]
        public String namekh { get; set; }
        public String sex { get; set; }
        public String phone { get; set; }
        public DateTime dob { get; set; }
        public String address { get; set; }
        public String email { get; set; }
        public String identityno { get; set; }

        public String photo { get; set; }
        public String shippertype { get; set; }
        public String vehiracle { get; set; }
        public String plateno { get; set; }
        public decimal phone_card { get; set; }
        public decimal petroluem { get; set; }
        public decimal deliveryin { get; set; }
        public decimal deliveryout { get; set; }
        public Boolean status { get; set; }
        public String createby { get; set; }
        public DateTime createdate { get; set; }
    }
}