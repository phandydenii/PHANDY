using AutoMapper;
using Microsoft.AspNet.Identity;
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
    public class SalarysController : ApiController
    {
        private ApplicationDbContext _context;
        public SalarysController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/GetSalarys  for get all record
        [HttpGet]
        public IHttpActionResult GetSalarys()
        {
            var salarys = _context.Salarys.ToList().Select(Mapper.Map<Salary, SalaryDto>);
            return Ok(salarys);
        }

        //GET : /api/GetSalarys/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetGetSalarys(int id)
        {
            var salarys = _context.Salarys.SingleOrDefault(c => c.salaryId == id);
            if (salarys == null)
                return NotFound();

            return Ok(Mapper.Map<Salary, SalaryDto>(salarys));
        }

        //POS : /api/GetSalarys   for Insert record
        [HttpPost]
        public IHttpActionResult CreateGetSalarys(SalaryDto SalaryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var salarys = Mapper.Map<SalaryDto, Salary>(SalaryDto);
            _context.Salarys.Add(salarys);
            salarys.createBy = User.Identity.GetUserName();
            salarys.createDate = DateTime.Now;
            _context.SaveChanges();
            SalaryDto.salaryId = salarys.salaryId;

            return Created(new Uri(Request.RequestUri + "/" + SalaryDto.salaryId), SalaryDto);


        }

        //PUT : /api/GetSalarys/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateGetSalarys(int id, SalaryDto SalaryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Salarys.SingleOrDefault(c => c.Name == BranchDto.Name && c.Id != BranchDto.Id);
            //if (isExists != null)
            //    return BadRequest();
            var salaryInDb = _context.Salarys.SingleOrDefault(c => c.salaryId == id);
            Mapper.Map(SalaryDto, salaryInDb);
            salaryInDb.createBy = User.Identity.GetUserName();
            salaryInDb.createDate = DateTime.Now;
            _context.SaveChanges();
            return Ok(SalaryDto);

        }

        //DELETE : /api/GetSalarys/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteGetSalarys(int id)
        {
            var salaryInDb = _context.Salarys.SingleOrDefault(c => c.salaryId == id);
            if (salaryInDb == null)
                return BadRequest();

            _context.Salarys.Remove(salaryInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
