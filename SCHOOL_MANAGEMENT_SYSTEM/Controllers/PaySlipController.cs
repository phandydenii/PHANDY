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
    public class PaySlipController : Controller
    {
        private ApplicationDbContext _context;

        public PaySlipController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        // GET: PaySlip
        public ActionResult Index()
        {

            var staffviewmodel = new StaffViewModel()
            {
                StaffList = _context.Staffs.ToList(),
            };

            return View(staffviewmodel);
        }
    }
}
