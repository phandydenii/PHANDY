using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class TransferController : Controller
    {
        private ApplicationDbContext _context;
        public TransferController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Employees
        [System.Web.Mvc.Route("Transfer")]
        public ActionResult Index()
        {
            var employeeViewModel = new TransferViewModel()
            {
                Showrooms = _context.Showroom.ToList()
            };

            return View(employeeViewModel);

        }
    }
}
