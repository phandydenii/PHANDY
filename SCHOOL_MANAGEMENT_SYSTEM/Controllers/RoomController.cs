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
                RoomTypes=_context.RoomTypes.ToList(),
                Buildings=_context.Buildings.ToList(),
                Floors=_context.Floors.ToList(),
                Rooms =_context.Rooms.ToList(),
                TotalBlock =_context.Rooms.Where(c => c.status== "BLOCK").Count(),
                TotalBook =_context.Rooms.Where(c => c.status == "BOOKING").Count(),
                TotalFree =_context.Rooms.Where(c => c.status == "FREE").Count(),
                TotalCheckIn =_context.Rooms.Where(c => c.status == "CHECKIN").Count(),
                Items =_context.Items.ToList(),
            };
            return View(roomViewModel);
        }
    }
}


