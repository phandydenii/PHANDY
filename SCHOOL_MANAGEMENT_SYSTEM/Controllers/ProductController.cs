using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class ProductController : Controller
    {
        private ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        // GET: Employees
        [System.Web.Mvc.Route("Product")]
        public ActionResult Index()
        {
            var employeeViewModel = new ProductViewModel()
            {
                
                Showrooms = _context.Showroom.ToList(),
                Categorys = _context.Category.ToList()

            };

            return View(employeeViewModel);

        }
    }
}
