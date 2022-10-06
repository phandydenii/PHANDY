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
    public class GradesController : ApiController
    {
        private ApplicationDbContext _context;
        public GradesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Grades  for get all record
        [HttpGet]
        public IHttpActionResult GetGrade()
        {
            var grades = _context.Grades.ToList().Select(Mapper.Map<grade, gradeDto>);
            return Ok(grades);
        }

        //GET : /api/Grades/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetBranchs(int id)
        {
            var grades = _context.Grades.SingleOrDefault(c => c.gradeid == id);
            if (grades == null)
                return NotFound();

            return Ok(Mapper.Map<grade, gradeDto>(grades));
        }

        //POS : /api/Grades   for Insert record
        [HttpPost]
        public IHttpActionResult CreateBranchs(gradeDto GradeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Grades.SingleOrDefault(c => c.gradename == GradeDto.gradename);
            if (isExists != null)
                return BadRequest();

            GradeDto.createdate = DateTime.Today;
            GradeDto.createby = User.Identity.GetUserName();

            var grade = Mapper.Map<gradeDto, grade>(GradeDto);
            _context.Grades.Add(grade);
            _context.SaveChanges();
            GradeDto.gradeid = grade.gradeid;

            return Created(new Uri(Request.RequestUri + "/" + GradeDto.gradeid), GradeDto);


        }

        //PUT : /api/Grades/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateBranch(int id, gradeDto GradeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Grades.SingleOrDefault(c => c.gradename == GradeDto.gradename && c.gradeid != GradeDto.gradeid);
            if (isExists != null)
                return BadRequest();
            var BranchInDb = _context.Grades.SingleOrDefault(c => c.gradeid == id);

            GradeDto.createdate = DateTime.Today;
            GradeDto.createby = User.Identity.GetUserName();

            Mapper.Map(GradeDto, BranchInDb);
            _context.SaveChanges();
            return Ok(GradeDto);

        }

        //DELETE : /api/Brachs/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id)
        {
            var BranchInDb = _context.Grades.SingleOrDefault(c => c.gradeid == id);
            if (BranchInDb == null)
                return BadRequest();

            var isExists = _context.Students.Where(c => c.studentgradeid == id).Take(1);
            if (isExists.Count() > 0)
                return BadRequest();

            _context.Grades.Remove(BranchInDb);
            _context.SaveChanges();
            return Ok(new { });


        }


    }
}
