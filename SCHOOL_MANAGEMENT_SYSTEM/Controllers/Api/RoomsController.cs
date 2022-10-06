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
    public class RoomsController : ApiController
    {
        private ApplicationDbContext _context;

        public RoomsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        //Get : api/Rooms
        public IHttpActionResult GetRoom()
        {
            var getRoom = (from r in _context.Rooms
                           join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                           join f in _context.Floors on r.floorid equals f.id
                           select new RoomV
                           {
                               id = r.id,
                               room_no = r.room_no,
                               roomtypeid = r.roomtypeid,
                               roomtypename = rt.roomtypename,
                               floorid = r.floorid,
                               floorno = f.floor_no,
                               servicecharge = r.servicecharge,
                               price = r.price,
                               roomkey = r.roomkey,
                               status = r.status
                           }).ToList();

            return Ok(getRoom);

        }
        [HttpGet]
        //Get : api/Rooms
        public IHttpActionResult GetRoomById(int id)
        {
            var getRoomByid = (from r in _context.Rooms
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               select new RoomV
                               {
                                   id = r.id,
                                   room_no = r.room_no,
                                   roomtypeid = r.roomtypeid,
                                   roomtypename = rt.roomtypename,
                                   floorid = r.floorid,
                                   floorno = f.floor_no,
                                   servicecharge = r.servicecharge,
                                   price = r.price,
                                   roomkey = r.roomkey,
                                   status = r.status
                               }).Where(c => c.id==id).SingleOrDefault();

            return Ok(getRoomByid);

        }
        [HttpPost]
        //Get : api/Rooms
        public IHttpActionResult CreateRoom(RoomDto RoomDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var RoomInDb = Mapper.Map<RoomDto, Room>(RoomDto);
            _context.Rooms.Add(RoomInDb);
            _context.SaveChanges();

            RoomInDb.id = RoomInDb.id;
            
            return Created(new Uri(Request.RequestUri + "/" + RoomDto.id), RoomDto);


        }
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, RoomDto RoomDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExist = _context.Vacancies.SingleOrDefault(c => c.VacancyName == VacancyDtos.VacancyName);
            //if (isExist != null)
            //    return BadRequest();
            var RoominDb = _context.Rooms.SingleOrDefault(c => c.id == id);
            Mapper.Map(RoomDto, RoominDb);
            _context.SaveChanges();

            return Ok(RoominDb);

        }

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteUser(int id)
        {
            var bookinfInDb = _context.Rooms.SingleOrDefault(c => c.id == id);
            if (bookinfInDb == null)
                return NotFound();
            _context.Rooms.Remove(bookinfInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
