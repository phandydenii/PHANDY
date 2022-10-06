using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext _context;
        public InvoiceController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Students
        [Route("Invoice")]
        public ActionResult Index()
        {
            var Payment = new InvoiceViewModel()
            {
                Showrooms = _context.Showroom.ToList(),
                Customers = _context.Customer.Where(c=>c.status==true).ToList(),
                Locations=_context.Location.ToList(),
                Products=_context.Product.ToList(),
                Employees=_context.Employee.Where(c=>c.status==true).ToList(),
            };

            return View(Payment);

        }
    }
}
