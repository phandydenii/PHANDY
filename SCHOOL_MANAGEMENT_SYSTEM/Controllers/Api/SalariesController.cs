using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class SalariesController : ApiController
    {
        private ApplicationDbContext _context;

        public SalariesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        //Get : api/RoomTypes{id}
        public IHttpActionResult GetSalary()
        {
            var GetSalary = _context.Salary.Include(s => s.staff).ToList();
            return Ok(GetSalary);
        }

        [HttpGet]
        //Get : api/RoomTypes{id}
        public IHttpActionResult GetSalaryById(int id)
        {
            var getStaffById = _context.Salary.SingleOrDefault(c => c.id == id);

            if (getStaffById == null)
                return NotFound();

            return Ok(Mapper.Map<Salary, SalaryDto>(getStaffById));
        }


    }
}
