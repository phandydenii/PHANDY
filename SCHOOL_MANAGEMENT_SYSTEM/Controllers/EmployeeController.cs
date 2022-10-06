using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class EmployeeController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Employees
        [System.Web.Mvc.Route("Employee")]
        public ActionResult Index()
        {
            var employeeViewModel = new EmployeeViewModel()
            {
                Position = _context.Position.ToList(),
                Department = _context.Departments.ToList(),
                Showroom = _context.Showroom.ToList()

            };

            return View(employeeViewModel);

        }
    }
}
