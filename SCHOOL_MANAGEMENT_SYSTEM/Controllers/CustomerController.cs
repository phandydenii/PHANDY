using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class CustomerController : Controller
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Employees
        [System.Web.Mvc.Route("Customer")]
        public ActionResult Index()
        {
            var employeeViewModel = new CustomerViewModel()
            {
                //Position = _context.Position.ToList(),
                //Department = _context.Departments.ToList(),
                Showrooms = _context.Showroom.ToList(),
                Customers = _context.Customer.ToList()

            };

            return View(employeeViewModel);

        }
    }
}
