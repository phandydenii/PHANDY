using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class PaymentViewModel
    {
        public IEnumerable<grade> Grades { get; set; }
        public IEnumerable<student> students { get; set; }
        public IEnumerable<shifts> Shifts { get; set; }
        public IEnumerable<ExchangeRate> Exchanges { get; set; }


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
        public string userid { get; set; }
        public int shiftid { get; set; }
        public int gradeid { get; set; }
        public decimal deposit { get; set; }
        public decimal depositr { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public List<ListItems> Items { get; set; }
    }
    public class ListItems
    {
        public int id { get; set; }
        public int paymentid { get; set; }
        public int courseid { get; set; }
        public int qty { get; set; }
        public decimal turtionfee { get; set; }
        public decimal discount { get; set; }
        public decimal total { get; set; }
    }
}