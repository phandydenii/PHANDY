using AutoMapper;
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
        [Route("api/invmaxid")]
        public IHttpActionResult GetInvoiceMaxID()
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(id) IS NULL THEN (1)ELSE MAX(id)+1 END AS ID,'RL'+ RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(id) IS NULL THEN (1)ELSE MAX(id)+1 END )) , 6) AS BookingNo FROM invoice_tbl", conx);
            adp.Fill(ds);
            string BookingNo = ds.Rows[0][1].ToString();
            return Ok(BookingNo);

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
                               status = r.status,
                               note=r.note,
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
                               where r.id==id
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
                                   status = r.status,
                                   note=r.note,
                               }).SingleOrDefault();

            return Ok(getRoomByid);

        }
        [HttpPost]
        //Get : api/Rooms
        public IHttpActionResult CreateRoom(RoomDto RoomDtos)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            var RoomInDb = Mapper.Map<RoomDto, Room>(RoomDtos);
            _context.Rooms.Add(RoomInDb);
            _context.SaveChanges();

            RoomDtos.id = RoomInDb.id;
            
            return Created(new Uri(Request.RequestUri + "/" + RoomDtos.id), RoomDtos);
        }


        [HttpPut]
        [Route("api/updateroomstatus/{id}/{status}")]
        public IHttpActionResult GetMaxID(int id,string status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
     
            SqlCommand requestcommand = new SqlCommand("Update room_tbl set status='"+ status + "' where id=" + id,conx);
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

        [HttpPut]
        [Route("api/blockroom/{id}")]
        public IHttpActionResult BlockRoom(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            var note = HttpContext.Current.Request.Form["note"];
            var status = HttpContext.Current.Request.Form["status"];
            var notestr = note +" "+ DateTime.Today;
            SqlCommand requestcommand = new SqlCommand("Update room_tbl set status='"+ status + "', note='" + notestr + "' where id=" + id, conx);
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



        //PUT
        [HttpPut]
        public IHttpActionResult UpdateUser(int id, RoomDto RoomDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExist = _context.Rooms.SingleOrDefault(c => c.room_no == RoomDtos.room_no);
            //if (isExist != null)
            //    return BadRequest();

            var RoominDb = _context.Rooms.SingleOrDefault(c => c.id == id);
            Mapper.Map(RoomDtos, RoominDb);
            _context.SaveChanges();
            return Ok(RoomDtos);
        }

        [HttpDelete]
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
