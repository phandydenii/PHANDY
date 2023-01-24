using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Data;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
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
                Rooms = _context.Rooms.Include(f => f.floor).ToList(),
                ListRoomFree = _context.Rooms.Where(c => c.status == "FREE").ToList(),
                ListRoomBook = _context.Rooms.Where(c => c.status == "BOOK").ToList(),
                TotalBlock = _context.Rooms.Where(c => c.status == "BLOCK").Count(),
                TotalBook = _context.Rooms.Where(c => c.status == "BOOK").Count(),
                TotalFree = _context.Rooms.Where(c => c.status == "FREE").Count(),
                TotalCheckIn = _context.Rooms.Where(c => c.status == "CHECK-IN").Count(),
                Items = _context.Items.ToList(),
                TotalRoom = _context.Rooms.Count(),
                ExchangeRateID = _context.Exchanges.Where(d => d.IsDeleted == false).Max(a => a.id),
                WaterPowerPriceID = _context.WEPrices.Where(d => d.IsDeleted == false).Max(a => a.id),
                GuestBook = _context.Guests.Where(c => c.status == "BOOK").ToList(),
                GuestList = _context.Guests.ToList(),
                TotalFloor=_context.Floors.ToList().OrderByDescending(x => x.id),
                TotalRoomFloor=_context.Rooms.ToList(),
                Room0 = _context.Rooms.Where(c =>c.floorid==1).OrderBy(x =>x.room_no).ToList(),
                RoomF1=_context.Rooms.Where(c=>c.floorid==2).OrderBy(x => x.room_no).ToList().ToList(),
                RoomF2=_context.Rooms.Where(c=>c.floorid==3).OrderBy(x => x.room_no).ToList().ToList(),
                RoomF3=_context.Rooms.Where(c=>c.floorid==4).OrderBy(x => x.room_no).ToList().ToList(),
                RoomF4=_context.Rooms.Where(c=>c.floorid==5).OrderBy(x => x.room_no).ToList().ToList(),
                RoomF5=_context.Rooms.Where(c=>c.floorid==6).OrderBy(x => x.room_no).ToList().ToList(),
       
            };
            return View(roomViewModel);
        }
    }
}



