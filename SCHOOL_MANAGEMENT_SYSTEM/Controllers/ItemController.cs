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
    public class ItemController : Controller
    {
        
        // GET: Items
        public ActionResult Index()
        {
            return View();
        }
    }
}
