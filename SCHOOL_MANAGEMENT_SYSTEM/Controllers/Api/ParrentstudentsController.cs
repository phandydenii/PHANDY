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
    public class ParrentstudentsController : ApiController
    {
        private ApplicationDbContext _context;
        public ParrentstudentsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        
        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetParrentstudents(int id)
        {
            var parents = _context.Parrentstudents.SingleOrDefault(c => c.parrentId == id);
            if (parents == null)
                return NotFound();

            return Ok(Mapper.Map<Parrentstudent, ParrentstudentDto>(parents));

        }


        //POS : /api/Parents   for Insert record
        [HttpPost]
        public IHttpActionResult CreateParentstudent(ParrentstudentDto ParentstutDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Parents.SingleOrDefault(c => c.fatherName == ParentDto.fatherName);
            //if (isExists != null)
            //    return BadRequest();

            var parent = Mapper.Map<ParrentstudentDto, Parrentstudent>(ParentstutDto);
            parent.createBy = User.Identity.GetUserName();
            parent.createDate = DateTime.Now;
            _context.Parrentstudents.Add(parent);
            _context.SaveChanges();
            ParentstutDto.parrentId = parent.parrentId;

            return Created(new Uri(Request.RequestUri + "/" + ParentstutDto.parrentId), ParentstutDto);


        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateParentstudent(int id, ParrentstudentDto ParentstudentDto){
           
            var ParentInDb = _context.Parrentstudents.SingleOrDefault(c => c.parrentId == id);
            Mapper.Map(ParentstudentDto, ParentInDb);
            //ParentInDb.parrentStuId = DBNull;
            ParentInDb.createBy = User.Identity.GetUserName();
            ParentInDb.createDate = DateTime.Now;
            _context.SaveChanges();
            return Ok(ParentstudentDto);

        }


        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id)
        {
            var ParentInDb = _context.Parrentstudents.SingleOrDefault(c => c.parrentId == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.Parrentstudents.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
