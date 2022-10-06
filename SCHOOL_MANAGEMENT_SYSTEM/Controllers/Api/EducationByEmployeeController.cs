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
    public class EducationByEmployeeController : ApiController
    {


        private ApplicationDbContext _context;
        public EducationByEmployeeController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        public IHttpActionResult GetEducations(int id)
        {
            var educations = _context.Educations.Where(c => c.educationEmpid == id);
            if (educations == null)
                return NotFound();

            return Ok(educations);
        }
    }
}
