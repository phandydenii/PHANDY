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
    public class PositionController : ApiController
    {
       

            private ApplicationDbContext _context;
            public PositionController()
            {
                _context = new ApplicationDbContext();
            }

            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }
        //GET : /api/Position  for get all record
        [HttpGet]
            public IHttpActionResult GetPosition()
            {
                var position = _context.Position.ToList().Select(Mapper.Map<Position, PositionDto>);
                return Ok(position);
            }

        //GET : /api/Position/{id} for get record by id
        [HttpGet]
            public IHttpActionResult GetPosition(int id)
            {
                var position = _context.Position.SingleOrDefault(c => c.id == id);
                if (position == null)
                    return NotFound();

                return Ok(Mapper.Map<Position, PositionDto>(position));
            }

        //POS : /api/position   for Insert record
        [HttpPost]
            public IHttpActionResult CreatePosition(PositionDto PositionDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var isExists = _context.Position.SingleOrDefault(c => c.positionname == PositionDto.positionname);
                if (isExists != null)
                    return BadRequest();
                    PositionDto.status = true;

                    var position = Mapper.Map<PositionDto, Position>(PositionDto);
                    _context.Position.Add(position);
                    _context.SaveChanges();
                    PositionDto.id = position.id;

                    return Created(new Uri(Request.RequestUri + "/" + PositionDto.id), PositionDto);


            }

            //PUT : /api/Position/{id}  for Update record
            [HttpPut]
            public IHttpActionResult UpdatePosition(int id, PositionDto PositionDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest();

                var isExists = _context.Position.SingleOrDefault(c => c.positionname == PositionDto.positionname && c.id != PositionDto.id);
                if (isExists != null)
                    return BadRequest();
                var PositionInDb = _context.Position.SingleOrDefault(c => c.id == id);
                PositionDto.status = true;
                //PositionDto.create_by = User.Identity.GetUserName();
                Mapper.Map(PositionDto, PositionInDb);
                _context.SaveChanges();
                return Ok(PositionDto);

            }

            //DELETE : /api/Departments/{id}  for Delete record
            [HttpDelete]
            public IHttpActionResult DeletePosition(int id)
            {
                var PositionInDb = _context.Position.SingleOrDefault(c => c.id == id);
                if (PositionInDb == null)
                    return BadRequest();

                _context.Position.Remove(PositionInDb);
                _context.SaveChanges();
                return Ok(new { });


            }
        }
}
