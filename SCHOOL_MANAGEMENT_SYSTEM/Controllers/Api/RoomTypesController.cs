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
    public class RoomTypesController : ApiController
    {
        private ApplicationDbContext _context;

        public RoomTypesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/RoomTypes
        public IHttpActionResult GetRoomType()
        {
            var getRoomType = _context.RoomTypes.ToList().Select(Mapper.Map<RoomType, RoomTypeDto>);

            return Ok(getRoomType);
        }


        [HttpGet]
        //Get : api/RoomTypes{id}
        public IHttpActionResult GetRoomType(int id)
        {
            var getRoomTypeById = _context.RoomTypes.SingleOrDefault(c => c.id == id);

            if (getRoomTypeById == null)
                return NotFound();

            return Ok(Mapper.Map<RoomType, RoomTypeDto>(getRoomTypeById));
        }
        [HttpPost]
        public IHttpActionResult CreateRoomType(RoomTypeDto RoomTypeDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.RoomTypes.SingleOrDefault(c => c.roomtypename == RoomTypeDtos.roomtypename);
            if (isExist != null)
                return BadRequest();

            var RoomType = Mapper.Map<RoomTypeDto, RoomType>(RoomTypeDtos);

            _context.RoomTypes.Add(RoomType);
            _context.SaveChanges();

            RoomTypeDtos.id = RoomType.id;

            return Created(new Uri(Request.RequestUri + "/" + RoomTypeDtos.id), RoomTypeDtos);
        }

        [HttpPut]
        //PUT : /api/RoomType/{id}
        public IHttpActionResult EditRoomType(int id, RoomTypeDto RoomTypeDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.RoomTypes.SingleOrDefault(c => c.roomtypename == RoomTypeDtos.roomtypename && c.id != RoomTypeDtos.id);
            if (isExist != null)
                return BadRequest();

            var RoomTypeInDb = _context.RoomTypes.SingleOrDefault(c => c.id == id);
            Mapper.Map(RoomTypeDtos, RoomTypeInDb);
            _context.SaveChanges();

            return Ok(RoomTypeDtos);
        }

        [HttpDelete]
        //PUT : /api/RoomTypes/{id}
        public IHttpActionResult DeleteRoomType(int id)
        {

            var RoomTypeInDb = _context.RoomTypes.SingleOrDefault(c => c.id == id);
            if (RoomTypeInDb == null)
                return NotFound();
            _context.RoomTypes.Remove(RoomTypeInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
