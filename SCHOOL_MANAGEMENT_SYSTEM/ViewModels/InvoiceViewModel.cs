using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{

    public class InvoiceViewModel
    {
        public IEnumerable<Customer> Customers { get; set; }
        public IEnumerable<Showroom> Showrooms { get; set; }
        public IEnumerable<Location> Locations { get; set; }
        public IEnumerable<Product> Products { get; set; }
        public IEnumerable<Employee> Employees { get; set; }
        public IEnumerable<ExchangeRate> Exchanges { get; set; }

        public int id { get; set; }
        public int invoiceno { get; set; }
        public DateTime date { get; set; }
        public int customerid { get; set; }
        public int showroomid { get; set; }
        public int exchangeid { get; set; }
        public decimal totalamount { get; set; }
        public decimal  totalcarprice { get; set; }
        public decimal totalshipprice { get; set; }
        public decimal  alreadypaid { get; set; }
        public string status { get; set; }
        public string createby { get; set; }
        public DateTime createdate { get; set; }
        public Boolean paid { get; set; }
        public Boolean alreadymove { get; set; }
        public List<ListItemss> Items { get; set; }
    }
    public class ListItemss
    {
        public int id { get; set; }
        public int invoiceid { get; set; }
        public int invoiceno { get; set; }
        public int locationid { get; set; }
        public int productid { get; set; }
        public int deliveryid { get; set; }
        public string deliverytype { get; set; }
        public string receiverphone { get; set; }
        public decimal price { get; set; }
        public decimal carprice { get; set; }
        public decimal shiprice { get; set; }
        public Boolean status { get; set; }

    }
}