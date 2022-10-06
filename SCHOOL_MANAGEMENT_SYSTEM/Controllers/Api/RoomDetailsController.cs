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
    public class RoomDetailsController : ApiController
    {
        private ApplicationDbContext _context;

        public RoomDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        //Get : api/RoomDetails
        public IHttpActionResult GetRoomDetail()
        {
            var getRoom = (from rd in _context.RoomDetails
                           join i in _context.Items on rd.itemid equals i.id
                           join r in _context.Rooms on rd.roomid equals r.id
                           join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                           join f in _context.Floors on r.floorid equals f.id
                           join b in _context.Buildings on f.buildingid equals b.id

                           select new RoomDetailV
                           {
                               id = rd.id,
                               roomid = r.id,
                               room_no = r.room_no,
                               roomtypeid = r.roomtypeid,
                               roomtypename = rt.roomtypename,
                               floorid = r.floorid,
                               floorno = f.floor_no,
                               buildingid=b.id,
                               buildingname=b.buildingname,
                               servicecharge = r.servicecharge,
                               price = r.price,
                               roomkey = r.roomkey,
                               status = r.status,
                               itemid=i.id,
                               itemname=i.itemname,
                               itemnamekh=i.itemnamekh
                               
                           }).ToList();

            return Ok(getRoom);

        }
        [HttpGet]
        //Get : api/Rooms
        public IHttpActionResult GetRoomDetailById(int id)
        {
            var getRoom = (from rd in _context.RoomDetails
                           join i in _context.Items on rd.itemid equals i.id
                           join r in _context.Rooms on rd.roomid equals r.id
                           join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                           join f in _context.Floors on r.floorid equals f.id
                           join b in _context.Buildings on f.buildingid equals b.id
                           select new RoomDetailV
                           {
                               id = rd.id,
                               roomid = r.id,
                               room_no = r.room_no,
                               roomtypeid = r.roomtypeid,
                               roomtypename = rt.roomtypename,
                               floorid = r.floorid,
                               floorno = f.floor_no,
                               buildingid = b.id,
                               buildingname = b.buildingname,
                               servicecharge = r.servicecharge,
                               price = r.price,
                               roomkey = r.roomkey,
                               status = r.status,
                               itemid = i.id,
                               itemname = i.itemname,
                               itemnamekh = i.itemnamekh

                           }).Where(c=>c.id==id).ToList();

            return Ok(getRoom);

        }
        [HttpGet]
        //Get : api/Rooms
        [Route("api/roomdetails/0/{roomid}")]
        public IHttpActionResult GetRoomDetailByRoomId(int roomid)
        {
            var getRoom = (from rd in _context.RoomDetails
                           join i in _context.Items on rd.itemid equals i.id
                           join r in _context.Rooms on rd.roomid equals r.id
                           join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                           join f in _context.Floors on r.floorid equals f.id
                           join b in _context.Buildings on f.buildingid equals b.id
                           where rd.roomid==roomid
                           select new RoomDetailV
                           {
                               id = rd.id,
                               roomid = r.id,
                               room_no = r.room_no,
                               roomtypeid = r.roomtypeid,
                               roomtypename = rt.roomtypename,
                               floorid = r.floorid,
                               floorno = f.floor_no,
                               buildingid = b.id,
                               buildingname = b.buildingname,
                               servicecharge = r.servicecharge,
                               price = r.price,
                               roomkey = r.roomkey,
                               status = r.status,
                               itemid = i.id,
                               itemname = i.itemname,
                               itemnamekh = i.itemnamekh

                           }).ToList();

            return Ok(getRoom);

        }
        [HttpPost]
        //Get : api/Rooms
        public IHttpActionResult CreateRoomDetail(RoomDetailDto RoomDetailDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var RoomDetailInDb = Mapper.Map<RoomDetailDto, RoomDetail>(RoomDetailDto);
            _context.RoomDetails.Add(RoomDetailInDb);
            _context.SaveChanges();

            RoomDetailInDb.id = RoomDetailInDb.id;

            return Created(new Uri(Request.RequestUri + "/" + RoomDetailDto.id), RoomDetailDto);


        }
        [HttpPut]
        public IHttpActionResult UpdateRoomDetail(int id, RoomDetailDto RoomDetailDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            
            var RoomDetailInDb = _context.RoomDetails.SingleOrDefault(c => c.id == id);
            Mapper.Map(RoomDetailDto, RoomDetailInDb);
            _context.SaveChanges();

            return Ok(RoomDetailInDb);

        }

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteDetail(int id)
        {
            var roomDetailInDb = _context.RoomDetails.SingleOrDefault(c => c.id == id);
            if (roomDetailInDb == null)
                return NotFound();
            _context.RoomDetails.Remove(roomDetailInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
