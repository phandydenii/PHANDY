using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class InvoiceController : Controller
    {
        private ApplicationDbContext _context;
        public InvoiceController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Students
        [Route("Invoice")]
        public ActionResult Index()
        {
            var Payment = new InvoiceViewModel()
            {
                ExchangeRateID = _context.Exchanges.Where(d => d.IsDeleted == false).Max(a => a.id),
                WaterPowerPriceID = _context.WaterPowerPrices.Where(d => d.IsDeleted == false).Max(a => a.id),
                ItemList = _context.Items.ToList(),
            };
            return View(Payment);
        }
    }
}
