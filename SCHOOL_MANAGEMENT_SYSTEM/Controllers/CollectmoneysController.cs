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
        public class CollectmoneysController : Controller
        {
            private ApplicationDbContext _context;
            public CollectmoneysController()
            {
                _context = new ApplicationDbContext();
            }

            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }

            // GET: Employees
            [System.Web.Mvc.Route("CollectMoney")]
            public ActionResult Index()
            {
                var employeeViewModel = new CollectMoneyViewModel()
                {
                    Employee = _context.Employee.ToList()
                };

                return View(employeeViewModel);

            }
        }
    }
