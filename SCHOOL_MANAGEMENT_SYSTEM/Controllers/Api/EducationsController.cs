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
    public class EducationsController : ApiController
    {
        private ApplicationDbContext _context;
        public EducationsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Educations  for get all record
        [HttpGet]
        public IHttpActionResult GetEducations()
        {
            var educations = _context.Educations.ToList().Select(Mapper.Map<Education, EducationDto>);
            return Ok(educations);
        }

        //GET : /api/Educations/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetEducations(int id)
        {
            var educations = _context.Educations.SingleOrDefault(c => c.educationId == id);
            if (educations == null)
                return NotFound();

            return Ok(Mapper.Map<Education, EducationDto>(educations));
        }

        //POS : /api/Educations   for Insert record
        [HttpPost]
        public IHttpActionResult CreateEducations(EducationDto EducationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Educations.SingleOrDefault(c => c.Name == BranchDto.Name);
            //if (isExists != null)
            //    return BadRequest();

            var education = Mapper.Map<EducationDto, Education>(EducationDto);
            education.createBy = User.Identity.GetUserName();
            education.createDate = DateTime.Now;
            _context.Educations.Add(education);
            _context.SaveChanges();
            EducationDto.educationId = education.educationId;

            return Created(new Uri(Request.RequestUri + "/" + EducationDto.educationId), EducationDto);


        }

        //PUT : /api/Educations/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateEducations(int id, EducationDto EducationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Branchs.SingleOrDefault(c => c.Name == BranchDto.Name && c.Id != BranchDto.Id);
            //if (isExists != null)
            //    return BadRequest();
            var EducationInDb = _context.Educations.SingleOrDefault(c => c.educationId == id);
            Mapper.Map(EducationDto, EducationInDb);
            EducationInDb.createBy = User.Identity.GetUserName();
            EducationInDb.createDate = DateTime.Now;
            _context.SaveChanges();
            return Ok(EducationDto);

        }

        //DELETE : /api/Educations/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteEducations(int id)
        {
            var EducationInDb = _context.Educations.SingleOrDefault(c => c.educationId == id);
            if (EducationInDb == null)
                return BadRequest();

            _context.Educations.Remove(EducationInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
