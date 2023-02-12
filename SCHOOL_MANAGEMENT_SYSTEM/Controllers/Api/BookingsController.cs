using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class BookingsController : ApiController
    {
        private ApplicationDbContext _context;

        public BookingsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/bookinginvoiceno")]
        public IHttpActionResult GetBookingMaxID()
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(id) IS NULL THEN (1)ELSE MAX(id)+1 END AS ID,'RL'+ RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(id) IS NULL THEN (1)ELSE MAX(id)+1 END )) , 6) AS BookingNo FROM booking_tbl", conx);
            adp.Fill(ds);
            string BookingNo = ds.Rows[0][1].ToString();
            return Ok(BookingNo);

        }


        [HttpGet]
        [Route("api/bookingmaxid")]
        public IHttpActionResult GetMaxID()
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id)+1 from booking_tbl", conx);
            adp.Fill(ds);
            string ChcekInId = ds.Rows[0][0].ToString();
            return Ok(ChcekInId);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBookings()
        {
            var getBuilding = _context.Bookings.Include(g => g.guest).Include(r => r.room).ToList().Select(Mapper.Map<Booking, BookingDto>);
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBookings(int id)
        {
            var getBuildingById = _context.Bookings.Include(g => g.guest).Include(r => r.room).SingleOrDefault(c => c.id == id);
            
            return Ok(getBuildingById);
        }
        

        [HttpGet]
        //Get : api/BookingDetails
        [Route("api/booking-v/{status}")]
        public IHttpActionResult GetRoomDetail(string status)
        {
            if (status == "All")
            {
                var getBookin = (from b in _context.Bookings
                                 join g in _context.Guests on b.guestid equals g.id
                                 join r in _context.Rooms on b.roomid equals r.id
                                 join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                                 select new BookingV
                                 {
                                     id = b.id,
                                     bookingdate = b.bookingdate,
                                     total = b.total,
                                     paydollar = b.paydollar,
                                     payriel = b.payriel,
                                     checkindate = b.checkindate,
                                     expirecheckindate = b.expiredate,
                                     bookstatus = b.status,
                                     note = b.note,
                                     roomid = r.id,
                                     room_no = r.room_no,
                                     roomtypeid = r.roomtypeid,
                                     roomtypename = rt.roomtypename,
                                     servicecharge = r.servicecharge,
                                     roomprice = r.price,
                                     roomkey = r.roomkey,
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
                                     gueststatus = g.status

                                 }).ToList();

                return Ok(getBookin);
            }else
            {
                var getBookin = (from b in _context.Bookings
                                 join g in _context.Guests on b.guestid equals g.id
                                 join r in _context.Rooms on b.roomid equals r.id
                                 join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                                 where b.status==status
                                 select new BookingV
                                 {
                                     id = b.id,
                                     bookingdate = b.bookingdate,
                                     total = b.total,
                                     paydollar = b.paydollar,
                                     payriel = b.payriel,
                                     checkindate = b.checkindate,
                                     expirecheckindate = b.expiredate,
                                     bookstatus = b.status,
                                     note = b.note,
                                     roomid = r.id,
                                     room_no = r.room_no,
                                     roomtypeid = r.roomtypeid,
                                     roomtypename = rt.roomtypename,
                                     servicecharge = r.servicecharge,
                                     roomprice = r.price,
                                     roomkey = r.roomkey,
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
                                     gueststatus = g.status

                                 }).ToList();

                return Ok(getBookin);
            }
        }

        [HttpGet]
        [Route("api/booking_v/{id}")]
        //Get : api/Rooms
        public IHttpActionResult GetRoomDetailById(int id)
        {
            var getBookin = (from b in _context.Bookings
                             join g in _context.Guests on b.guestid equals g.id
                             join r in _context.Rooms on b.roomid equals r.id
                             join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                             where b.id==id
                             select new BookingV
                             {
                                 id = b.id,
                                 bookingdate = b.bookingdate,
                                 total = b.total,
                                 paydollar = b.paydollar,
                                 payriel = b.payriel,
                                 checkindate = b.checkindate,
                                 expirecheckindate = b.expiredate,
                                 note = b.note,
                                 roomid = r.id,
                                 room_no = r.room_no,
                                 roomtypeid = r.roomtypeid,
                                 roomtypename = rt.roomtypename,
                                 servicecharge = r.servicecharge,
                                 roomprice = r.price,
                                 roomkey = r.roomkey,
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
                                 gueststatus = g.status

                             }).ToList();

            return Ok(getBookin);
        }

        [HttpGet]
        //Get : api/Rooms
        public IHttpActionResult GetRoomDetailByRoomID(int roomid)
        {
            var getBookin = (from b in _context.Bookings
                             join g in _context.Guests on b.guestid equals g.id
                             join r in _context.Rooms on b.roomid equals r.id
                             join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                             where r.id == roomid && g.status=="BOOK" && b.status=="Active" orderby b.id descending 
                             select new BookingV
                             {
                                 id = b.id,
                                 bookingdate = b.bookingdate,
                                 total = b.total,
                                 paydollar = b.paydollar,
                                 payriel = b.payriel,
                                 checkindate = b.checkindate,
                                 expirecheckindate = b.expiredate,
                                 note = b.note,
                                 roomid = r.id,
                                 room_no = r.room_no,
                                 roomtypeid = r.roomtypeid,
                                 roomtypename = rt.roomtypename,
                                 servicecharge = r.servicecharge,
                                 roomprice = r.price,
                                 roomkey = r.roomkey,
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
                                 gueststatus = g.status

                             }).Take(1);

            return Ok(getBookin);
        }


        [HttpPut]
        [Route("api/bookingstatus/{id}/{status}")]
        public IHttpActionResult GetMaxID(int id,string status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand requestcommand = new SqlCommand("Update booking_tbl set status='"+ status + "' where  id=" + id, conx);
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

        

        [HttpPost]
        //Get : api/Rooms
        public IHttpActionResult CreateRoomDetail(BookingDto BookingDtos)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand requestcommand = new SqlCommand("select max(id) from booking_tbl", conx);
            SqlDataAdapter da = new SqlDataAdapter("select max(id) from ExchangeRates where IsDeleted=0", conx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string eid = dt.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var BookinInDb = Mapper.Map<BookingDto, Booking>(BookingDtos);
            BookinInDb.bookingdate = DateTime.Today;
            BookinInDb.userid = User.Identity.GetUserId();
            BookinInDb.updateby= User.Identity.GetUserId();
            BookinInDb.updatedate = DateTime.Today;
            BookinInDb.exchangeid = int.Parse(eid);
            BookinInDb.status = "Active";

            _context.Bookings.Add(BookinInDb);
            _context.SaveChanges();

            BookingDtos.id = BookinInDb.id;

            Int16 BookMax;
            try
            {
                conx.Open();
                BookMax = Convert.ToInt16(requestcommand.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(BookMax);
        }

        [HttpPut]
        public IHttpActionResult UpdateUser(int id, BookingDto BookinDtos)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand requestcommand = new SqlCommand("select max(id) from ExchangeRates where IsDeleted=0", conx);
            SqlDataAdapter da = new SqlDataAdapter("select max(id) from ExchangeRates where IsDeleted=0", conx);
            DataTable dt = new DataTable();
            da.Fill(dt);
            string eid = dt.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var BookingInDb = _context.Bookings.SingleOrDefault(c => c.id == id);
            Mapper.Map(BookinDtos, BookingInDb);
            BookingInDb.userid = User.Identity.GetUserId();
            BookingInDb.updateby = User.Identity.GetUserId();
            BookingInDb.updatedate = DateTime.Today;
            BookingInDb.exchangeid = int.Parse(eid);
            _context.SaveChanges();
            return Ok(BookingInDb);
        }

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteDetail(int id)
        {
            var roomDetailInDb = _context.Bookings.SingleOrDefault(c => c.id == id);
            if (roomDetailInDb == null)
                return NotFound();
            _context.Bookings.Remove(roomDetailInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}

