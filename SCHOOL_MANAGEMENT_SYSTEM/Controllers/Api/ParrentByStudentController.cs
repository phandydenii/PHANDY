using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class ParrentByStudentController : ApiController
    {
        private ApplicationDbContext _context;
        public ParrentByStudentController()
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
            var parents = _context.Parrentstudents.Where(c => c.parrentStuId == id).ToList();
            if (parents == null)
                return NotFound();

            return Ok(parents);
        }
    }
}
