using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class EmployeesController : Controller
    {
        private ApplicationDbContext _context;
        public EmployeesController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Employees
        [System.Web.Mvc.Route("Employees")]
        public ActionResult Index()
        {
            var employeeViewModel = new EmployeesViewModel()
            {
                Branchs = _context.Branchs.ToList(),
                Departments = _context.Departments.ToList(),
                
            };

            return View(employeeViewModel);
            
        }
    }
}