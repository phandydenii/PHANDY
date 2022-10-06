using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class RegisterstudentsController : Controller
    {
        private ApplicationDbContext _context;
        public RegisterstudentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Students
        [Route("Registerstudents")]
        public ActionResult Index()
        {
            var registerViewModel = new RegisterstudentViewModel()
            {
                students = _context.Students.ToList(),
                studylanguages = _context.studylanguages.ToList(),
                studyperiods = _context.studyperiods.ToList(),
                shifts = _context.Shiftes.ToList(),
                grads = _context.Grades.ToList()

            };

            return View(registerViewModel);

        }
    }
}
