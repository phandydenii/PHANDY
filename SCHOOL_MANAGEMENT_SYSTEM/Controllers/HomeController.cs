using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
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
            var countModel = new CountModel()
            {
                TotalUser = _context.Users.Count(),
                TotalExpense = _context.Exchanges.Count(),
                TotalGuest = _context.Guests.Count(),
                TotalStaff = _context.Staffs.Count(),
                TotalItem = _context.Items.Count(),
                TotalcheckIn = _context.CheckIns.Count(),
                TotalRoom=_context.Rooms.Count(),
                TotalBook=_context.Bookings.Count(),
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
            return View();
        }

    }
}