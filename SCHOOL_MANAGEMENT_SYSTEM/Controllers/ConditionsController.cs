using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class ConditionsController : Controller
    {
        private ApplicationDbContext _context;

        public ConditionsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Conditions
        public ActionResult Index()
        {
            var employee = _context.Employee.ToList();

            return View(employee);
        }
    }
}