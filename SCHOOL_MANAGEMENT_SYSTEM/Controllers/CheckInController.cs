using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

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
            //var roomViewModel = new RoomViewModel()
            //{
            //    RoomTypes = _context.RoomTypes.ToList(),
            //    Buildings = _context.Buildings.ToList(),
            //    Floors = _context.Floors.ToList(),
            //    Rooms = _context.Rooms.ToList(),
            //    TotalBlock = _context.Rooms.Where(c => c.status == "BLOCK").Count(),
            //    TotalBook = _context.Rooms.Where(c => c.status == "BOOKING").Count(),
            //    TotalFree = _context.Rooms.Where(c => c.status == "FREE").Count(),
            //    Items = _context.Items.ToList(),
            //};
            return View();
        }
    }
}
