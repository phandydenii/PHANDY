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
    public class BranchsController : ApiController
    {
        private ApplicationDbContext _context;
        public BranchsController() {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Branchs  for get all record
        [HttpGet]
        public IHttpActionResult GetBranchs() {
            var branchs = _context.Branchs.ToList().Select(Mapper.Map<Branch, BranchDto>);
            return Ok(branchs);
        }

        //GET : /api/Branchs/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetBranchs(int id)
        {
            var branchs = _context.Branchs.SingleOrDefault(c=>c.Id==id);
            if (branchs == null)
                return NotFound();

            return Ok(Mapper.Map<Branch,BranchDto>(branchs));
        }

        //POS : /api/Branchs   for Insert record
        [HttpPost]
        public IHttpActionResult CreateBranchs(BranchDto BranchDto) {
            if (!ModelState.IsValid) 
                return BadRequest();

            var isExists = _context.Branchs.SingleOrDefault(c => c.Name == BranchDto.Name);
            if (isExists != null)
                return BadRequest();

            BranchDto.create_date = DateTime.Today;
            BranchDto.create_by = User.Identity.GetUserName();

            var branch = Mapper.Map<BranchDto, Branch>(BranchDto);
            _context.Branchs.Add(branch);
            _context.SaveChanges();
            BranchDto.Id = branch.Id;

            return Created(new Uri(Request.RequestUri + "/" + BranchDto.Id), BranchDto);


        }

        //PUT : /api/Branchs/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateBranch(int id,BranchDto BranchDto) {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Branchs.SingleOrDefault(c => c.Name == BranchDto.Name && c.Id != BranchDto.Id);
            if (isExists != null)
                return BadRequest();
            var BranchInDb = _context.Branchs.SingleOrDefault(c => c.Id == id);

            BranchDto.create_date = DateTime.Today;
            BranchDto.create_by = User.Identity.GetUserName();

            Mapper.Map(BranchDto, BranchInDb);
            _context.SaveChanges();
            return Ok(BranchDto);

        }

        //DELETE : /api/Brachs/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id) {
            var BranchInDb = _context.Branchs.SingleOrDefault(c => c.Id==id);
            if (BranchInDb == null)
                return BadRequest();

            var isExists = _context.Employees.Where(c => c.BranchId == id).Take(1);
            if (isExists.Count() > 0)
                return BadRequest();

            _context.Branchs.Remove(BranchInDb);
            _context.SaveChanges();
            return Ok(new { });


        }


    }
}
