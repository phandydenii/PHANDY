using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class EmergencyByStudentController : ApiController
    {


        private ApplicationDbContext _context;
        public EmergencyByStudentController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        public IHttpActionResult GetEmergency(int id)
        {
            var salary = _context.Emergencys.Where(c => c.emerstudentid == id);
            if (salary == null)
                return NotFound();

            return Ok(salary);
        }
    }
}
