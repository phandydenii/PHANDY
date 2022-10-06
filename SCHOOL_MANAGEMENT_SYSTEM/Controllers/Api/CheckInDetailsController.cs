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
    public class CheckInDetailsController : ApiController
    {
        private ApplicationDbContext _context;

        public CheckInDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInDetail()
        {
            var getBuilding = _context.CheckInDetails.ToList().Select(Mapper.Map<CheckInDetail, CheckInDetailDto>);
            return Ok(getBuilding);
        }
        [HttpGet]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInDetailById(int id)
        {
            var getBuilding = _context.CheckInDetails.Where(c => c.id==id).ToList().Select(Mapper.Map<CheckInDetail, CheckInDetailDto>);
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/CheckIns
        [Route("api/checkindetail_v")]
        public IHttpActionResult GetCheckInDetail_V()
        {
            var getCheckInDetailByid = (from cd in _context.CheckInDetails
                                  join c in _context.CheckIns on cd.checkinid equals c.id
                                  join g in _context.Guests on c.guestid equals g.id
                                  join r in _context.Rooms on cd.roomid equals r.id
                                  join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                                  join f in _context.Floors on r.floorid equals f.id
                                  join b in _context.Buildings on f.buildingid equals b.id


                                        select new CheckInDetailV
                                  {
                                      id = c.id,
                                      checkinid=c.id,
                                      checkindate = c.checkindate,
                                      startdate = c.startdate,
                                      enddate = c.enddate,
                                      userid = c.userid,
                                      roomid = r.id,
                                      roomno = r.room_no,
                                      roomkey=r.roomkey,
                                      servicecharge=r.servicecharge,
                                      roomprice=r.price,
                                      roomtypeid = r.roomtypeid,
                                      roomtypename = rt.roomtypename,
                                      roomtypenamekh = rt.roomtypenamekh,
                                      floorid = r.floorid,
                                      floorno = f.floor_no,
                                      buildingid = b.id,
                                      buildingname = b.buildingname,
                                      guestid = c.guestid,
                                      guestname = g.name,
                                      guestnamekh = g.namekh,
                                      sex=g.sex,
                                      dob=g.dob,
                                      address=g.address,
                                      nationality=g.nationality,
                                      phone=g.phone,
                                      email=g.email,
                                      ssn=g.ssn,
                                      passport=g.passport,
                                      child = c.child,
                                      man = c.man,
                                      women = c.women,
                                      status=g.status
                                  }).SingleOrDefault();

            return Ok(getCheckInDetailByid);

        }
        [HttpGet]
        //Get : api/CheckIns
        [Route("api/checkindetail_v/{roomid}")]
        public IHttpActionResult GetCheckInDetail_V(int roomid)
        {
            var getCheckInDetailByid = (from cd in _context.CheckInDetails
                                        join c in _context.CheckIns on cd.checkinid equals c.id
                                        join g in _context.Guests on c.guestid equals g.id
                                        join r in _context.Rooms on cd.roomid equals r.id
                                        join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                                        join f in _context.Floors on r.floorid equals f.id
                                        join b in _context.Buildings on f.buildingid equals b.id

                                        where (r.id==roomid)
                                        select new CheckInDetailV
                                        {
                                            id = c.id,
                                            checkinid = c.id,
                                            checkindate = c.checkindate,
                                            startdate = c.startdate,
                                            enddate = c.enddate,
                                            userid = c.userid,
                                            roomid = r.id,
                                            roomno = r.room_no,
                                            roomkey=r.roomkey,
                                            servicecharge=r.servicecharge,
                                            roomprice=r.price,
                                            roomtypeid = r.roomtypeid,
                                            roomtypename = rt.roomtypename,
                                            roomtypenamekh = rt.roomtypenamekh,
                                            floorid = r.floorid,
                                            floorno = f.floor_no,
                                            buildingid = b.id,
                                            buildingname = b.buildingname,
                                            guestid = c.guestid,
                                            guestname = g.name,
                                            guestnamekh = g.namekh,
                                            sex = g.sex,
                                            dob = g.dob,
                                            address = g.address,
                                            nationality = g.nationality,
                                            phone = g.phone,
                                            email = g.email,
                                            ssn = g.ssn,
                                            passport = g.passport,
                                            child = c.child,
                                            man = c.man,
                                            women = c.women,
                                            status=g.status
                                            

                                        }).SingleOrDefault();

            return Ok(getCheckInDetailByid);

        }

        [HttpPost]
        //Get : api/CheckIns
        public IHttpActionResult CreateCheckIn(CheckInDto CheckInDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            //var isExist = _context.Vacancies.SingleOrDefault(c => c.VacancyName == VacancyDtos.VacancyName);
            //if (isExist != null)
            //    return BadRequest();

            var CheckInInDb = Mapper.Map<CheckInDto, CheckIn>(CheckInDto);
            _context.CheckIns.Add(CheckInInDb);
            _context.SaveChanges();

            CheckInInDb.id = CheckInInDb.id;

            return Created(new Uri(Request.RequestUri + "/" + CheckInDto.id), CheckInDto);


        }
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, CheckInDto CheckInDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExist = _context.Vacancies.SingleOrDefault(c => c.VacancyName == VacancyDtos.VacancyName);
            //if (isExist != null)
            //    return BadRequest();
            var CheckIninDb = _context.CheckIns.SingleOrDefault(c => c.id == id);
            Mapper.Map(CheckInDto, CheckIninDb);
            _context.SaveChanges();

            return Ok(CheckIninDb);

        }

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteUser(int id)
        {
            var bookinfInDb = _context.CheckIns.SingleOrDefault(c => c.id == id);
            if (bookinfInDb == null)
                return NotFound();
            _context.CheckIns.Remove(bookinfInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
