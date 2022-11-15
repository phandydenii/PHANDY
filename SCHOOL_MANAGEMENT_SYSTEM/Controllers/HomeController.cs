using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            var currentUser = User.Identity.GetUserId();
            var userOne = _context.Users.SingleOrDefault(c => c.Id == currentUser);
            //var brandId = userOne.BrandId;
            var countModel = new CountModel()
            {
                TotalUser = _context.Users.Count(),
                TotalEmployee = _context.Employee.Where(c => c.status == true).Count(),
                //TotalInvoice = _context.Invoice.Where(c => c.status == true).Count(),
                TotalCustomer = _context.Customer.Where(c => c.status == true).Count(),
                TotalBonus = _context.Bonus.Count(),
                TotalExpense = _context.Exchanges.Count(),
                TotalTransfer = _context.Transfers.Count(),
                TotalBalance = _context.Customer.Where(c => c.status == true).Count(),
                TotalGuest = _context.Guests.Count(),
                TotalStaff = _context.Staffs.Count(),
                TotalItem = _context.Items.Count(),
                TotalcheckIn = _context.CheckIns.Count(),
                TotalRoom=_context.Rooms.Count(),
            };


            return View(countModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}