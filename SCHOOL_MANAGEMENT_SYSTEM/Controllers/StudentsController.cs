using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class StudentsController : Controller
    {
       
        private ApplicationDbContext _context;
        public StudentsController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Students
        [Route("Students")]
        public ActionResult Index()
        {
            var studentViewModel = new StudentViewModel()
            {
                Branchs = _context.Branchs.ToList(),
                Shifts=_context.Shiftes.ToList(),
                Grades=_context.Grades.ToList()

            };

            return View(studentViewModel);

        }
    }
}