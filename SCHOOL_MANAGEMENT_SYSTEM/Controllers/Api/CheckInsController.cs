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
using System.Web;
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
        //Get : api/CheckIns
        public IHttpActionResult GetAllCheckIn()
        {
            var getCheckIn = (from c in _context.CheckIns
                              join r in _context.Rooms on c.roomid equals r.id
                              join g in _context.Guests on c.guestid equals g.id
                              where g.status== "CHECK-IN"
                              select new CheckInV
                              {
                                  id = c.id,
                                  checkindate = c.checkindate,
                                  roomid = r.id,
                                  room_no = r.room_no,
                                  servicecharge = r.servicecharge,
                                  price = r.price,
                                  roomkey = r.roomkey,
                                  roomstatus = r.status,
                                  payforroom = c.payforroom,
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
                                  gueststatus = g.status,
                                  child = c.child,
                                  man = c.man,
                                  women = c.women
                              }
                              ).ToList();
            return Ok(getCheckIn);
        }

        [HttpGet]
        //Get : api/CheckIns
        public IHttpActionResult GetAllCheckIn(int id)
        {
            var getCheckIn = (from c in _context.CheckIns
                              join g in _context.Guests on c.guestid equals g.id
                              join we in _context.WaterEletricUsages on g.id equals we.guestid
                              join r in _context.Rooms on c.roomid equals r.id
                              
                            
                              select new CheckInV
                              {
                                  id=c.id,
                                  checkindate=c.checkindate,
                                  roomid=r.id,
                                  room_no=r.room_no,
                                  servicecharge=r.servicecharge,
                                  price=r.price,
                                  roomkey=r.roomkey,
                                  roomstatus=r.status,
                                  startdate=we.startdate,
                                  enddate=we.enddate,
                                  wstartrecode=we.wstartrecord,
                                  estartrecord=we.estartrecord,
                                  payforroom=c.payforroom,
                                  guestid=g.id,
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
                                  gueststatus=g.status,
                                  child=c.child,
                                  man=c.man,
                                  women=c.women
                              }
                              ).ToList();
            return Ok(getCheckIn);
        }


        [HttpGet]
        [Route("api/checkin_v")]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckIn()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from CheckIn_v", conx);
            adp.Fill(ds);
            return Ok(ds.Tables[0]);
        }

        [HttpGet]
        [Route("api/checkin_v/{id}")]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInV(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from CheckIn_v where id=" + id, conx);
            adp.Fill(ds);
            return Ok(ds.Tables[0]);
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
                              join we in _context.WaterEletricUsages on g.id equals we.guestid
                              join wep in _context.WEPrices on we.wepriceid equals wep.id
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
                                  startdate = we.startdate,
                                  enddate = we.enddate,
                                  wstartrecode = we.wstartrecord,
                                  wendrecord = we.wendrecord,
                                  estartrecord = we.estartrecord,
                                  eendrecord = we.eendrecord,
                                  payforroom=c.payforroom,
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
                              }).ToList();
            return Ok(getCheckIn);
        }

        [HttpGet]
        [Route("api/checkinbyroom-v/{roomid}")]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInVByRoomIDs(int roomid)
        {
            var getCheckIn = (from c in _context.CheckIns
                              join r in _context.Rooms on c.roomid equals r.id
                              join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                              join g in _context.Guests on c.guestid equals g.id
                              join f in _context.Floors on r.floorid equals f.id
                              join b in _context.Buildings on f.buildingid equals b.id
                              where r.id == roomid && g.status== "CheckIn"
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
                                  payforroom=c.payforroom,
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

        [HttpPost]
        //Get : api/CheckIns
        public IHttpActionResult CreateCheckIn(CheckInDto CheckInDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var CheckInInDb = Mapper.Map<CheckInDto, CheckIn>(CheckInDto);
            CheckInInDb.checkindate = DateTime.Today;
            CheckInInDb.startdate = DateTime.Today;
            CheckInInDb.enddate = DateTime.Today;
            CheckInInDb.userid = User.Identity.GetUserId();
            CheckInInDb.active = false;

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
        public IHttpActionResult UpdateUser(int id, CheckInDto CheckInDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var CheckIninDb = _context.CheckIns.SingleOrDefault(c => c.id == id);
            if (CheckIninDb == null)
                return BadRequest();
            Mapper.Map(CheckInDtos, CheckIninDb);
            CheckIninDb.userid = User.Identity.GetUserId();
          
            _context.SaveChanges();

            return Ok(CheckInDtos);
        }

        [HttpPut]
        [Route("api/updatecheckind/{id}")]
        public IHttpActionResult UpdateUser(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update checkin_tbl set active=1 where id=" + id, conx);
            try
            {
                conx.Open();
                requestcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok();
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


        [HttpPut]
        [Route("api/checkin/{id}")]
        public IHttpActionResult UpdateCheckIn(int id)
        {
            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            var startdate = DateTime.Parse(HttpContext.Current.Request.Form["startdate"]);
            var enddate = DateTime.Parse(HttpContext.Current.Request.Form["enddate"]);
            var child = int.Parse(HttpContext.Current.Request.Form["child"]);
            var man = int.Parse(HttpContext.Current.Request.Form["man"]);
            var women = int.Parse(HttpContext.Current.Request.Form["women"]);

            SqlCommand command = new SqlCommand();
            SqlCommand requestcommand = new SqlCommand();
            requestcommand.Connection = conx;
            requestcommand.CommandType = CommandType.StoredProcedure;
            requestcommand.CommandText = "UPDATE_CHECKIN";
            requestcommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            requestcommand.Parameters.Add("@startdate", SqlDbType.Date).Value = startdate;
            requestcommand.Parameters.Add("@enddate", SqlDbType.Date).Value = enddate;
            requestcommand.Parameters.Add("@child", SqlDbType.Int).Value = child;
            requestcommand.Parameters.Add("@man", SqlDbType.Int).Value = man;
            requestcommand.Parameters.Add("@women", SqlDbType.Int).Value = women;


            try
            {
                conx.Open();
                requestcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }
    }
}
