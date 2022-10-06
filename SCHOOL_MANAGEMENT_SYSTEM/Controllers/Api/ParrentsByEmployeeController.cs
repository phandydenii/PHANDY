using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
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
    public class ParrentsByEmployeeController : ApiController
    {
       
       
            private ApplicationDbContext _context;
            public ParrentsByEmployeeController()
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
                var parents = _context.Parents.Where(c => c.parrentEmpId == id).ToList();
                if (parents == null)
                    return NotFound();

                return Ok(parents);
            }
        }
    
}
