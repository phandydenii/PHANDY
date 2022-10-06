using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class ShowroomController : ApiController
    {
        private ApplicationDbContext _context;
        public ShowroomController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET : /api/Position  for get all record
        [HttpGet]
        public IHttpActionResult GetShowroom()
        {
            var showroom = _context.Showroom.ToList().Select(Mapper.Map<Showroom, ShowroomDto>);
            return Ok(showroom);
        }

        //GET : /api/Position/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetShowroom(int id)
        {
            var position = _context.Showroom.SingleOrDefault(c => c.id == id);
            if (position == null)
                return NotFound();

            return Ok(Mapper.Map<Showroom, ShowroomDto>(position));
        }

        //POS : /api/position   for Insert record
        [HttpPost]
        public IHttpActionResult CreateShowroom(ShowroomDto showroomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Showroom.SingleOrDefault(c => c.name == showroomDto.name && c.status==true);
            if (isExists != null)
                return BadRequest();

            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;

            showroomDto.status = true;
            showroomDto.createby = createby;
            showroomDto.createdate = createdate;

            var position = Mapper.Map<ShowroomDto, Showroom>(showroomDto);
            _context.Showroom.Add(position);
            _context.SaveChanges();
            showroomDto.id = position.id;

            return Created(new Uri(Request.RequestUri + "/" + showroomDto.id), showroomDto);


        }

        //PUT : /api/Position/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateShowroom(int id, ShowroomDto showroomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Showroom.SingleOrDefault(c => c.name == showroomDto.name && c.id != showroomDto.id);
            if (isExists != null)
                return BadRequest();
            var PositionInDb = _context.Showroom.SingleOrDefault(c => c.id == id);
            showroomDto.status = true;
            showroomDto.createby = User.Identity.GetUserName();
            Mapper.Map(showroomDto, PositionInDb);
            _context.SaveChanges();
            return Ok(showroomDto);

        }

        //DELETE : /api/Departments/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteShowroom(int id)
        {
            var PositionInDb = _context.Showroom.SingleOrDefault(c => c.id == id);
            if (PositionInDb == null)
                return BadRequest();

            var isExists = _context.Employee.Where(c => c.showroomid == id).Take(1);
            if (isExists.Count() > 0)
                return BadRequest();


            _context.Showroom.Remove(PositionInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
