using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    public class PaymentV
    {
        [Key]
        public int id { get; set; }
        public int paymentno { get; set; }
        public DateTime paymentdate { get; set; }
        public int studentid { get; set; }
        
        public DateTime enrolldate { get; set; }
        public decimal adminfee { get; set; }
        public bool food { get; set; }
        public string duration { get; set; }
        public DateTime expireddate { get; set; }
        public int dayextend { get; set; }
        public string paymentstatus { get; set; }
        public int overdate { get; set; }
        public string note { get; set; }
        public int userid { get; set; }
        public int shiftid { get; set; }
        
        public int gradeid { get; set; }
        
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public string fullname { get; set; }
        public string fullnamekh { get; set; }
        public string shiftname { get; set; }
        public string gradename { get; set; }
    }
}