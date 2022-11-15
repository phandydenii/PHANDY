using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class RoomController : Controller
    {
        private ApplicationDbContext _context;

        public RoomController()
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
                RoomTypes = _context.RoomTypes.ToList(),
                Buildings = _context.Buildings.ToList(),
                Floors = _context.Floors.ToList(),
                Rooms = _context.Rooms.ToList(),
                ListRoomFree = _context.Rooms.Where(c => c.status == "FREE").ToList(),
                ListRoomBook = _context.Rooms.Where(c => c.status == "BOOK").ToList(),
                TotalBlock = _context.Rooms.Where(c => c.status == "BLOCK").Count(),
                TotalBook = _context.Rooms.Where(c => c.status == "BOOK").Count(),
                TotalFree = _context.Rooms.Where(c => c.status == "FREE").Count(),
                TotalCheckIn = _context.Rooms.Where(c => c.status == "CHECK-IN").Count(),
                Items = _context.Items.ToList(),
                TotalRoom = _context.Rooms.Count(),
                ExchangeRateID =_context.Exchanges.Where(d=>d.IsDeleted==false).Max(a=>a.id),

                GuestBook = _context.Guests.Where(c => c.status == "BOOK").ToList(),

            };
            return View(roomViewModel);
        }
    }
}



