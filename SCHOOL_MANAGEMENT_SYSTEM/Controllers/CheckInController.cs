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
    public class CheckInController : Controller
    {
        private ApplicationDbContext _context;

        public CheckInController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        // GET: Room
        public ActionResult Index()
        {
            var roomViewModel = new RoomViewModel()
            {
                ExchangeRateID = _context.Exchanges.Where(d => d.IsDeleted == false).Max(a => a.id),
                WaterPowerPriceID = _context.WaterPowerPrices.Where(d => d.IsDeleted == false).Max(a => a.id),
                GuestList = _context.Guests.ToList(),
                ItemList = _context.Items.ToList(),
            };
            return View(roomViewModel);
        }
    }
}
