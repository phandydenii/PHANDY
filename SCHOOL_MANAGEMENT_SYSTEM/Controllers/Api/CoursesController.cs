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
        public class CoursesController : ApiController
        {
            private ApplicationDbContext _context;
            public CoursesController()
            {
                _context = new ApplicationDbContext();

            }
            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }

            //GET : /api/Branchs  for get all record
            [HttpGet]
            public IHttpActionResult GetCourses()
            {
                var shifts = _context.courses.ToList().Select(Mapper.Map<course, courseDto>);
                return Ok(shifts);
            }

            //GET : /api/Branchs/{id} for get record by id
            [HttpGet]
            public IHttpActionResult GetCourses(int id)
            {
                var sh = _context.courses.SingleOrDefault(c => c.id == id);
                if (sh == null)
                    return NotFound();

                return Ok(Mapper.Map<course, courseDto>(sh));
            }

            //POS : /api/Branchs   for Insert record
            [HttpPost]
            public IHttpActionResult CreateCourses(courseDto courseDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var isExists = _context.courses.SingleOrDefault(c => c.coursecode == courseDto.coursecode);
                if (isExists != null)
                    return BadRequest();

                var shift = Mapper.Map<courseDto, course>(courseDto);
                _context.courses.Add(shift);
                shift.status = "ACTIVE";
                shift.createdate = DateTime.Today;
                shift.createby = User.Identity.GetUserName();
                _context.SaveChanges();
                courseDto.id = shift.id;

                return Created(new Uri(Request.RequestUri + "/" + courseDto.id), courseDto);


            }

            //PUT : /api/Branchs/{id}  for Update record
            [HttpPut]
            public IHttpActionResult UpdateCourse(int id, courseDto courseDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var isExists = _context.courses.SingleOrDefault(c => c.coursecode == courseDto.coursecode && c.id != courseDto.id);
                if (isExists != null)
                    return BadRequest();
                var shiftInDb = _context.courses.SingleOrDefault(c => c.id == id);

                shiftInDb.createdate = DateTime.Today;
                shiftInDb.createby = User.Identity.GetUserName();
                
                Mapper.Map(courseDto, shiftInDb);
                _context.SaveChanges();
                return Ok(courseDto);

            }

            //DELETE : /api/Brachs/{id}  for Delete record
            [HttpDelete]
            public IHttpActionResult DeleteBranch(int id)
            {
                var BranchInDb = _context.courses.SingleOrDefault(c => c.id == id);
                if (BranchInDb == null)
                    return BadRequest();

                _context.courses.Remove(BranchInDb);
                _context.SaveChanges();
                return Ok(new { });


            }


        }
    
}
