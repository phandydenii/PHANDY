using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class CheckInsController : ApiController
    {
        private ApplicationDbContext _context;

        public CheckInsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        

        [HttpGet]
        [Route("api/checkin_v")]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckIn()
        {
            var getCheckIn = (from c in _context.CheckIns
                           join r in _context.Rooms on c.roomid equals r.id
                           join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                           join g in _context.Guests on c.guestid equals g.id
                           join f in _context.Floors on r.floorid equals f.id
                           join b in _context.Buildings on f.buildingid equals b.id
                           select new CheckInV
                           {
                               id = c.id,
                               roomid=r.id,
                               room_no=r.room_no,
                               roomtypeid=rt.id,
                               roomtypename=rt.roomtypename,
                               floorid=f.id,
                               floorno=f.floor_no,
                               building=b.buildingname,
                               servicecharge=r.servicecharge,
                               price=r.price,
                               roomkey=r.roomkey,
                               roomstatus=r.status,
                               guestid = g.id,
                               name=g.name,
                               namekh=g.namekh,
                               sex=g.sex,
                               dob=g.dob,
                               address=g.address,
                               nationality=g.nationality,
                               phone=g.phone,
                               email=g.email,
                               ssn=g.ssn,
                               passport=g.passport,
                               child=c.child,
                               man=c.man,
                               women=c.women,
                               checkindate=c.checkindate,
                               startdate=c.startdate,
                               enddate=c.enddate
                              }).ToList();
            return Ok(getCheckIn);
        }

        [HttpGet]
        [Route("api/checkin_v/{id}")]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInV(int id)
        {
            var getCheckIn = (from c in _context.CheckIns
                              join r in _context.Rooms on c.roomid equals r.id
                              join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                              join g in _context.Guests on c.guestid equals g.id
                              join f in _context.Floors on r.floorid equals f.id
                              join b in _context.Buildings on f.buildingid equals b.id
                              where c.id==id
                              select new CheckInV
                              {
                                  id = c.id,
                                  roomid = r.id,
                                  room_no = r.room_no,
                                  roomtypeid = rt.id,
                                  roomtypename = rt.roomtypename,
                                  floorid = f.id,
                                  floorno = f.floor_no,
                                  building = b.buildingname,
                                  servicecharge = r.servicecharge,
                                  price = r.price,
                                  roomkey = r.roomkey,
                                  roomstatus = r.status,
                                  guestid = g.id,
                                  name = g.name,
                                  namekh = g.namekh,
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
                                  checkindate = c.checkindate,
                                  startdate = c.startdate,
                                  enddate = c.enddate
                              }).SingleOrDefault();
            return Ok(getCheckIn);
        }

        [HttpGet]
        [Route("api/checkinbyroom_v/{roomid}")]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInVByRoomID(int roomid)
        {
            var getCheckIn = (from c in _context.CheckIns
                              join r in _context.Rooms on c.roomid equals r.id
                              join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                              join g in _context.Guests on c.guestid equals g.id
                              join f in _context.Floors on r.floorid equals f.id
                              join b in _context.Buildings on f.buildingid equals b.id
                              where r.id == roomid
                              select new CheckInV
                              {
                                  id = c.id,
                                  roomid = r.id,
                                  room_no = r.room_no,
                                  roomtypeid = rt.id,
                                  roomtypename = rt.roomtypename,
                                  floorid = f.id,
                                  floorno = f.floor_no,
                                  building = b.buildingname,
                                  servicecharge = r.servicecharge,
                                  price = r.price,
                                  roomkey = r.roomkey,
                                  roomstatus = r.status,
                                  guestid = g.id,
                                  name = g.name,
                                  namekh = g.namekh,
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
                                  checkindate = c.checkindate,
                                  startdate = c.startdate,
                                  enddate = c.enddate
                              }).ToList();
            return Ok(getCheckIn);
        }

        [HttpGet]
        public IHttpActionResult GetCheckIns()
        {
            var getBuilding = _context.CheckIns.ToList().Select(Mapper.Map<CheckIn, CheckInDto>);
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInById(int id)
        {
            var getBuildingById = _context.CheckIns.SingleOrDefault(c => c.id == id);

            if (getBuildingById == null)
                return NotFound();
            return Ok(Mapper.Map<CheckIn, CheckInDto>(getBuildingById));
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
            //DbContextTransaction dbTran = _context.Database.BeginTransaction();

            var CheckInInDb = Mapper.Map<CheckInDto, CheckIn>(CheckInDto);
            CheckInInDb.checkindate = DateTime.Today;
            CheckInInDb.startdate = DateTime.Today;
            CheckInInDb.enddate= DateTime.Today;
            CheckInInDb.userid= User.Identity.GetUserName();

            _context.CheckIns.Add(CheckInInDb);
            _context.SaveChanges();

            CheckInInDb.id = CheckInInDb.id;

            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select max(id) from checkin_tbl", conx);

            Int16 checkInMaxID;
            try
            {
                conx.Open();
                cmd.ExecuteNonQuery();
                checkInMaxID = Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(checkInMaxID);

            //return Created(new Uri(Request.RequestUri + "/" + CheckInDto.id), CheckInDto);


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
