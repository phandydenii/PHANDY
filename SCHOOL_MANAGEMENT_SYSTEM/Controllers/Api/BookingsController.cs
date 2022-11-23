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
            var getBuilding = _context.Bookings.ToList().Select(Mapper.Map<Booking, BookingDto>);
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBookings(int id)
        {
            var getBuildingById = _context.Bookings.SingleOrDefault(c => c.id == id);

            if (getBuildingById == null)
                return NotFound();
            return Ok(Mapper.Map<Booking, BookingDto>(getBuildingById));
        }
        

        [HttpGet]
        //Get : api/BookingDetails
        [Route("api/booking_v")]
        public IHttpActionResult GetRoomDetail()
        {
            var getBookin = (from b in _context.Bookings
                           join g in _context.Guests on b.guestid equals g.id
                           join r in _context.Rooms on b.roomid equals r.id
                           join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                           
                           select new BookingV
                           {
                               id = b.id,
                               bookingno = b.bookingno,
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
                                 bookingno = b.bookingno,
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
                             where r.id == roomid && b.status=="Active" orderby b.id descending 
                             select new BookingV
                             {
                                 id = b.id,
                                 bookingno = b.bookingno,
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
        [Route("api/bookingstatus/{id}/cancel")]
        public IHttpActionResult GetMaxID(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("Update booking_tbl set status='Cancel' where  id=" + id, conx);
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

        //[HttpPost]
        //public IHttpActionResult BOOKING_INSERT()
        //{

        //    DataTable ds = new DataTable();
        //    DataTable ds1 = new DataTable();
        //    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SqlConnection conx = new SqlConnection(connectionString);
        //    SqlDataAdapter adp = new SqlDataAdapter("select max(id) from guest_tbl", conx);
        //    SqlDataAdapter adp1 = new SqlDataAdapter("select max(id) from ExchangeRates where IsDeleted=0", conx);
        //    adp.Fill(ds);
        //    adp1.Fill(ds1);

        //    string GuestID = ds.Rows[0][0].ToString();
        //    string ExchangeId = ds1.Rows[0][0].ToString();

        //    var bookingno = HttpContext.Current.Request.Form["bookingno"];
        //    var bookingdate = DateTime.Today;
        //    string UserId = User.Identity.GetUserId();
        //    var guestid = int.Parse(GuestID);
        //    var exchangeid = int.Parse(ExchangeId);
        //    var roomid = int.Parse(HttpContext.Current.Request.Form["roomid"]);
        //    var total = decimal.Parse(HttpContext.Current.Request.Form["total"]);
        //    var paydollar = decimal.Parse(HttpContext.Current.Request.Form["paydollar"]);
        //    var payriel = decimal.Parse(HttpContext.Current.Request.Form["payriel"]);
        //    var expiredate = DateTime.Parse(HttpContext.Current.Request.Form["expiredate"]);
        //    var note = HttpContext.Current.Request.Form["note"];
        //    var status ="ACTIVE";
        //    var updateby = User.Identity.GetUserName();
        //    var updatedate = DateTime.Today;
        //    var checkindate = DateTime.Today;


        //    SqlCommand command = new SqlCommand();
        //    SqlCommand requestcommand = new SqlCommand();
        //    requestcommand.Connection = conx;
        //    requestcommand.CommandType = CommandType.StoredProcedure;
        //    requestcommand.CommandText = "BOOKING_INSERT";
        //    requestcommand.Parameters.Add("@bookingno", SqlDbType.VarChar).Value = bookingno;
        //    requestcommand.Parameters.Add("@bookingdate", SqlDbType.Date).Value = bookingdate;
        //    requestcommand.Parameters.Add("@userid", SqlDbType.VarChar).Value = UserId;
        //    requestcommand.Parameters.Add("@guestid", SqlDbType.Int).Value = guestid;
        //    requestcommand.Parameters.Add("@roomid", SqlDbType.Int).Value = roomid;
        //    requestcommand.Parameters.Add("@exchangeid ", SqlDbType.Int).Value = exchangeid;
        //    requestcommand.Parameters.Add("@total", SqlDbType.Decimal).Value = total;
        //    requestcommand.Parameters.Add("@paydollar", SqlDbType.Decimal).Value = paydollar;
        //    requestcommand.Parameters.Add("@payriel", SqlDbType.Decimal).Value = payriel;
        //    requestcommand.Parameters.Add("@updateby", SqlDbType.VarChar).Value = updateby;
        //    requestcommand.Parameters.Add("@updatedate", SqlDbType.Date).Value = updatedate;
        //    requestcommand.Parameters.Add("@checkindate", SqlDbType.Date).Value = checkindate;
        //    requestcommand.Parameters.Add("@expirecheckindate", SqlDbType.Date).Value = expiredate;
        //    requestcommand.Parameters.Add("@note", SqlDbType.VarChar).Value = note;
        //    requestcommand.Parameters.Add("@status", SqlDbType.VarChar).Value = status;


        //    try
        //    {
        //        conx.Open();
        //        requestcommand.ExecuteNonQuery();
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Ok();
        //}


        [HttpPost]
        //Get : api/Rooms
        public IHttpActionResult CreateRoomDetail(BookingDto BookingDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var BookinInDb = Mapper.Map<BookingDto, Booking>(BookingDtos);
            BookinInDb.bookingdate = DateTime.Today;
            BookinInDb.userid = User.Identity.GetUserId();
            BookinInDb.updateby= User.Identity.GetUserId();
            BookinInDb.updatedate = DateTime.Today;

            _context.Bookings.Add(BookinInDb);
            _context.SaveChanges();

            BookingDtos.id = BookinInDb.id;

            return Created(new Uri(Request.RequestUri + "/" + BookingDtos.id), BookingDtos);
        }
        [HttpPut]
        public IHttpActionResult UpdateBookingDetail(int id, BookingDto BookingDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var RoomDetailInDb = _context.Bookings.SingleOrDefault(c => c.id == id);
            Mapper.Map(BookingDtos, RoomDetailInDb);
            _context.SaveChanges();

            return Ok(RoomDetailInDb);

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

