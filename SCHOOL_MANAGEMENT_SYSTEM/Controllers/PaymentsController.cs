using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class PaymentsController : Controller
    {
        private ApplicationDbContext _context;
        public PaymentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Students
        [Route("Payments")]
        public ActionResult Index()
        {
            var Payment = new PaymentViewModel()
            {
                students = _context.Students.ToList(),
                Shifts = _context.Shiftes.ToList(),
                Grades = _context.Grades.ToList()

            };

            return View(Payment);

        }
    }
}
