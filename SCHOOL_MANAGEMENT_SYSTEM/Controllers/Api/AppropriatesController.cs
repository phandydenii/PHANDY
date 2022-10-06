using AutoMapper;
using Microsoft.AspNet.Identity;
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
    public class AppropriatesController : ApiController
    {
        private ApplicationDbContext _context;
        public AppropriatesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetAppropriate(int id)
        {
            var parents = _context.appropriates.SingleOrDefault(c => c.appid == id);
            if (parents == null)
                return NotFound();

            return Ok(Mapper.Map<appropriate, appropriateDto>(parents));

        }


        //POS : /api/Parents   for Insert record
        [HttpPost]
        public IHttpActionResult CreateAppropriate(appropriateDto appropriateDto)
        {
            //if (!ModelState.IsValid)
            //    return BadRequest();

            //var isExists = _context.appropriates.SingleOrDefault(c => c.fatherName == appropriateDto.fatherName);
            //if (isExists != null)
            //    return BadRequest();

            var parent = Mapper.Map<appropriateDto, appropriate>(appropriateDto);
            parent.createby = User.Identity.GetUserName();
            parent.createdate = DateTime.Now;
            _context.appropriates.Add(parent);
            _context.SaveChanges();
            appropriateDto.appid = parent.appid;
            return Created(new Uri(Request.RequestUri + "/" + appropriateDto.appid), appropriateDto);
        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateAppropriate(int id, appropriateDto appropriateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ParentInDb = _context.appropriates.SingleOrDefault(c => c.appid == id);
            Mapper.Map(appropriateDto, ParentInDb);
            //ParentInDb.parrentStuId = DBNull;
            ParentInDb.createby = User.Identity.GetUserName();
            ParentInDb.createdate = DateTime.Now;
            _context.SaveChanges();
            return Ok(appropriateDto);

        }


        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteAppropriate(int id)
        {
            var ParentInDb = _context.appropriates.SingleOrDefault(c => c.appid == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.appropriates.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
