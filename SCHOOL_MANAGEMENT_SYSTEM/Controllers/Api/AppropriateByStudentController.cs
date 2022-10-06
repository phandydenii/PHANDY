using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class AppropriateByStudentController : ApiController
    {
        private ApplicationDbContext _context;
        public AppropriateByStudentController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        public IHttpActionResult GetParents(int id)
        {
            var parents = _context.appropriates.Where(c => c.appstudentid == id).ToList();
            if (parents == null)
                return NotFound();

            return Ok(parents);
        }
    }
}
