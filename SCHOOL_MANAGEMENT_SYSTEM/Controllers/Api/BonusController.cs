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
    public class BonusController : ApiController
    {
        private ApplicationDbContext _context;
        public BonusController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Branchs  for get all record
        [HttpGet]
        public IHttpActionResult GetBranchs()
        {
            var branchs = _context.Bonus.ToList().Select(Mapper.Map<Bonus, BonusDto>);
            return Ok(branchs);
        }

        //GET : /api/Branchs/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetBranchs(int id)
        {
            var branchs = _context.Bonus.SingleOrDefault(c => c.id == id);
            if (branchs == null)
                return NotFound();

            return Ok(Mapper.Map<Bonus, BonusDto>(branchs));
        }

        //POS : /api/Branchs   for Insert record
        [HttpPost]
        public IHttpActionResult CreateBranchs(BonusDto BranchDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Bonus.SingleOrDefault(c => c.Name == BranchDto.Name);
            //if (isExists != null)
            //    return BadRequest();

            BranchDto.createdate = DateTime.Today;
            BranchDto.createby = User.Identity.GetUserName();

            var branch = Mapper.Map<BonusDto, Bonus>(BranchDto);
            _context.Bonus.Add(branch);
            _context.SaveChanges();
            BranchDto.id = branch.id;

            return Created(new Uri(Request.RequestUri + "/" + BranchDto.id), BranchDto);


        }

        //PUT : /api/Branchs/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateBranch(int id, BonusDto bonusDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Branchs.SingleOrDefault(c => c.Name == BranchDto.Name && c.Id != BranchDto.Id);
            //if (isExists != null)
            //    return BadRequest();

            var BranchInDb = _context.Bonus.SingleOrDefault(c => c.id == id);

            bonusDto.createdate = DateTime.Today;
            bonusDto.createby = User.Identity.GetUserName();

            Mapper.Map(bonusDto, BranchInDb);
            _context.SaveChanges();
            return Ok(bonusDto);

        }

        //DELETE : /api/Brachs/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id)
        {
            var BranchInDb = _context.Bonus.SingleOrDefault(c => c.id == id);
            if (BranchInDb == null)
                return BadRequest();

            //var isExists = _context.Bonus.Where(c => c.BranchId == id).Take(1);
            //if (isExists.Count() > 0)
            //    return BadRequest();

            _context.Bonus.Remove(BranchInDb);
            _context.SaveChanges();
            return Ok(new { });


        }


    }
}
