using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
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

        [HttpGet]
        [Route("api/salaries/{a}/{staffid}")]
        //Get : api/RoomTypes{id}
        public IHttpActionResult GetSalaryByStaff(int a,int staffid)
        {
            var getStaffById = _context.Salary.SingleOrDefault(c => c.staffid == staffid);

            if (getStaffById == null)
                return NotFound();

            return Ok(Mapper.Map<Salary, SalaryDto>(getStaffById));
        }

        [HttpPost]
        public IHttpActionResult InsertSalary(SalaryDto SalaryDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.Salary.SingleOrDefault(c => c.staffid == SalaryDtos.staffid);
            if (isExist != null)
                return BadRequest();

            var SalaryInDB = Mapper.Map<SalaryDto, Salary>(SalaryDtos);
            SalaryInDB.date = DateTime.Today;
            SalaryInDB.createdate = DateTime.Today;
            SalaryInDB.createby = User.Identity.GetUserId();
            _context.Salary.Add(SalaryInDB);
            _context.SaveChanges();

            SalaryDtos.id = SalaryInDB.id;

            return Created(new Uri(Request.RequestUri + "/" + SalaryDtos.id), SalaryDtos);
        }

        [HttpPut]
        //PUT : /api/Building/{id}
        public IHttpActionResult UpdateSalary(int id)
        {
            var staffid = HttpContext.Current.Request.Form["staffid"];
            var salary = HttpContext.Current.Request.Form["salary"];
            var note = HttpContext.Current.Request.Form["note"];

            var empInDb = _context.Salary.SingleOrDefault(c => c.id == id);
            var payslipdDto = new SalaryDto()
            {
                staffid=int.Parse(staffid),
                salary=decimal.Parse(salary),
                note=note,
                date=DateTime.Today,
                createby=User.Identity.GetUserName(),
                createdate=DateTime.Today,
            };

            Mapper.Map(payslipdDto, empInDb);
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        //PUT : /api/Staffs/{id}
        public IHttpActionResult DeleteStaff(int id)
        {
            var StaffInDb = _context.Salary.SingleOrDefault(c => c.id == id);
            if (StaffInDb == null)
                return NotFound();
            _context.Salary.Remove(StaffInDb);
            _context.SaveChanges();
            return Ok(new { });
        }
    }
}
