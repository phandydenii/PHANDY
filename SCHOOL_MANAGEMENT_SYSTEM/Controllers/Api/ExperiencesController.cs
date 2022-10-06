using AutoMapper;
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
    public class ExperiencesController : ApiController
    {
        private ApplicationDbContext _context;
        public ExperiencesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Experiences  for get all record
        [HttpGet]
        public IHttpActionResult GetExperiences()
        {
            var experiences = _context.Experiences.ToList().Select(Mapper.Map<Experience, ExperienceDto>);
            return Ok(experiences);
        }

        //GET : /api/Experiences/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetExperiences(int id)
        {
            var experiences = _context.Experiences.SingleOrDefault(c => c.Id == id);
            if (experiences == null)
                return NotFound();

            return Ok(Mapper.Map<Experience, ExperienceDto>(experiences));
        }

        //POS : /api/Experiences   for Insert record
        [HttpPost]
        public IHttpActionResult CreateExperiences(ExperienceDto ExperienceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Educations.SingleOrDefault(c => c.Name == BranchDto.Name);
            //if (isExists != null)
            //    return BadRequest();

            var experiences = Mapper.Map<ExperienceDto, Experience>(ExperienceDto);
            _context.Experiences.Add(experiences);
            _context.SaveChanges();
            ExperienceDto.Id = experiences.Id;

            return Created(new Uri(Request.RequestUri + "/" + ExperienceDto.Id), ExperienceDto);


        }

        //PUT : /api/Experiences/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateExperiences(int id, ExperienceDto ExperienceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Branchs.SingleOrDefault(c => c.Name == BranchDto.Name && c.Id != BranchDto.Id);
            //if (isExists != null)
            //    return BadRequest();
            var ExperienceInDb = _context.Experiences.SingleOrDefault(c => c.Id == id);
            Mapper.Map(ExperienceDto, ExperienceInDb);
            _context.SaveChanges();
            return Ok(ExperienceInDb);

        }

        //DELETE : /api/Experiences/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteExperiences(int id)
        {
            var ExperienceInDb = _context.Experiences.SingleOrDefault(c => c.Id == id);
            if (ExperienceInDb == null)
                return BadRequest();

            _context.Experiences.Remove(ExperienceInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
