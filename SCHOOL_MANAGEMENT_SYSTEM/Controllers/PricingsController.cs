using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class PricingsController : Controller
    {
        private ApplicationDbContext _context;

        public PricingsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Pricings
        public ActionResult Index()
        {
            var employee = _context.Employee.ToList();

            return View(employee);
        }
    }
}