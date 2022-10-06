using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class ParentsController : ApiController
    {
        private ApplicationDbContext _context;
        public ParentsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Parents  for get all record
        [HttpGet]
        public IHttpActionResult GetParents()
        {
            var parents = _context.Parents.Include(c => c.employee).ToList().Select(Mapper.Map<Parent, ParentDto>);
            return Ok(parents);
        }

        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetParents(int id)
        {
            var parents = _context.Parents.SingleOrDefault(c => c.parrentId == id);
            if (parents == null)
                return NotFound();

            return Ok(Mapper.Map<Parent, ParentDto>(parents));
        }


        //POS : /api/Parents   for Insert record
        [HttpPost]
        public IHttpActionResult CreateParents(ParentDto ParentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Parents.SingleOrDefault(c => c.fatherName == ParentDto.fatherName);
            //if (isExists != null)
            //    return BadRequest();

            var parent = Mapper.Map<ParentDto, Parent>(ParentDto);
            parent.createBy = User.Identity.GetUserName();
            parent.createDate = DateTime.Now;
            _context.Parents.Add(parent);
            _context.SaveChanges();
            ParentDto.parrentId = parent.parrentId;

            return Created(new Uri(Request.RequestUri + "/" + ParentDto.parrentId), ParentDto);


        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateParent(int id, ParentDto ParentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

        
            var ParentInDb = _context.Parents.SingleOrDefault(c => c.parrentId == id);
            Mapper.Map(ParentDto, ParentInDb);
            ParentInDb.createBy = User.Identity.GetUserName();
            ParentInDb.createDate = DateTime.Now;
            _context.SaveChanges();
            return Ok(ParentDto);

        }

        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id)
        {
            var ParentInDb = _context.Parents.SingleOrDefault(c => c.parrentId == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.Parents.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
