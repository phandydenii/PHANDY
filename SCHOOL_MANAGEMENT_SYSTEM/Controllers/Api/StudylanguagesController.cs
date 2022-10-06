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
    public class StudylanguagesController : ApiController
    {
        private ApplicationDbContext _context;
        public StudylanguagesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Branchs  for get all record
        [HttpGet]
        public IHttpActionResult GetLanguage()
        {
            var shifts = _context.studylanguages.ToList().Select(Mapper.Map<studylanguage, studylanguageDto>);
            return Ok(shifts);
        }

        //GET : /api/Branchs/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetLanguage(int id)
        {
            var sh = _context.studylanguages.SingleOrDefault(c => c.studylanguageid == id);
            if (sh == null)
                return NotFound();

            return Ok(Mapper.Map<studylanguage, studylanguageDto>(sh));
        }

        //POS : /api/Branchs   for Insert record
        [HttpPost]
        public IHttpActionResult CreateLanguage(studylanguageDto studylanguageDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.studylanguages.SingleOrDefault(c => c.language == studylanguageDto.language);
            if (isExists != null)
                return BadRequest();

            var shift = Mapper.Map<studylanguageDto, studylanguage>(studylanguageDto);
            _context.studylanguages.Add(shift);
            shift.languagestatus = "active";
            shift.createdate = DateTime.Today;
            shift.createby = User.Identity.GetUserName();
            _context.SaveChanges();
            studylanguageDto.studylanguageid = shift.studylanguageid;

            return Created(new Uri(Request.RequestUri + "/" + studylanguageDto.studylanguageid), studylanguageDto);


        }

        //PUT : /api/Branchs/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateShift(int id, studylanguageDto ShiftDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.studylanguages.SingleOrDefault(c => c.language == ShiftDto.language && c.studylanguageid != ShiftDto.studylanguageid);
            if (isExists != null)
                return BadRequest();
            var shiftInDb = _context.studylanguages.SingleOrDefault(c => c.studylanguageid == id);

            shiftInDb.createdate = DateTime.Today;
            shiftInDb.createby = User.Identity.GetUserName();

            Mapper.Map(ShiftDto, shiftInDb);
            _context.SaveChanges();
            return Ok(ShiftDto);

        }

        //DELETE : /api/Brachs/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteLanguage(int id)
        {
            var BranchInDb = _context.studylanguages.SingleOrDefault(c => c.studylanguageid == id);
            if (BranchInDb == null)
                return BadRequest();

            //Check language already used at RegisterStudent or note
            var used = _context.Registerstudents.SingleOrDefault(c => c.languageid == id);
            if (used != null)
                return BadRequest();

            _context.studylanguages.Remove(BranchInDb);
            _context.SaveChanges();
            return Ok(new { });


        }


    }
}
