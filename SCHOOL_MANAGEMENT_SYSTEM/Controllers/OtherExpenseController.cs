using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class OtherExpenseController : Controller
    {
        private ApplicationDbContext _context;
        public OtherExpenseController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Employees
        [System.Web.Mvc.Route("OtherExpense")]
        public ActionResult Index()
        {
            var employeeViewModel = new OtherExpenseViewModel()
            {
                ExpenseTypes = _context.ExpenseTypes.ToList(),
            };

            return View(employeeViewModel);

        }
    }
}
