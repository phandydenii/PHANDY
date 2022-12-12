using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class SalaryController : Controller
    {
        private ApplicationDbContext _context;

        public SalaryController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var staffViewModel = new StaffViewModel()
            {
                StaffList = _context.Staffs.ToList(),

            };
            return View(staffViewModel);
        }
    }
}
