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
    public class GuestsController : ApiController
    {
        private ApplicationDbContext _context;

        public GuestsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/guestsmaxid/{id}")]
        public IHttpActionResult GetMaxID(int id)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select id from guest_tbl where id='"+id+"'", conx);
            adp.Fill(ds);
            string GuestID = ds.Rows[0][0].ToString();
            return Ok(GuestID);

        }

        [HttpGet]
        [Route("api/guestsmaxid")]
        public IHttpActionResult GetMaxID()
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from guest_tbl", conx);
            adp.Fill(ds);
            string GuestID = ds.Rows[0][0].ToString();
            return Ok(GuestID);

        }

        [HttpPut]
        [Route("api/updategueststatus/{id}/{status}")]
        public IHttpActionResult GetMaxID(int id, string status)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("Update guest_tbl set status='" + status + "' where id=" + id, conx);
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


        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetGuests()
        {
            var getGuest = _context.Guests.ToList().Select(Mapper.Map<Guest, GuestDto>);
            return Ok(getGuest);
        }

        [HttpGet]
        //Get : api/Floors{id}
        public IHttpActionResult GetGuestById(int id)
        {
            var getGuestById = _context.Guests.SingleOrDefault(c => c.id == id);

            if (getGuestById == null)
                return NotFound();

            return Ok(Mapper.Map<Guest, GuestDto>(getGuestById));
        }

        [HttpPost]
        public IHttpActionResult GUEST_INSERT()
        {

            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select max(id) from guest_tbl", conx);
            
            var name = HttpContext.Current.Request.Form["name"];
            var namekh = HttpContext.Current.Request.Form["namekh"];
            var sex = HttpContext.Current.Request.Form["sex"];
            var dob = HttpContext.Current.Request.Form["dob"];
            var address = HttpContext.Current.Request.Form["address"];
            var nationality = HttpContext.Current.Request.Form["nationality"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var email = HttpContext.Current.Request.Form["email"];
            var ssn = HttpContext.Current.Request.Form["ssn"];
            var passport = HttpContext.Current.Request.Form["passport"];
            var status = HttpContext.Current.Request.Form["status"];
           


            SqlCommand command = new SqlCommand();
            SqlCommand requestcommand = new SqlCommand();
            requestcommand.Connection = conx;
            requestcommand.CommandType = CommandType.StoredProcedure;
            requestcommand.CommandText = "INSERT_GUEST";
            requestcommand.Parameters.Add("@name", SqlDbType.VarChar).Value = name;
            requestcommand.Parameters.Add("@namekh", SqlDbType.NVarChar).Value = namekh;
            requestcommand.Parameters.Add("@sex", SqlDbType.VarChar).Value = sex;
            requestcommand.Parameters.Add("@dob", SqlDbType.Date).Value = dob;
            requestcommand.Parameters.Add("@address", SqlDbType.NVarChar).Value = address;
            requestcommand.Parameters.Add("@nationality", SqlDbType.NVarChar).Value = nationality;
            requestcommand.Parameters.Add("@phone", SqlDbType.VarChar).Value = phone;
            requestcommand.Parameters.Add("@email", SqlDbType.VarChar).Value = email;
            requestcommand.Parameters.Add("@ssn", SqlDbType.VarChar).Value = ssn;
            requestcommand.Parameters.Add("@passport", SqlDbType.VarChar).Value = passport;
            requestcommand.Parameters.Add("@status", SqlDbType.VarChar).Value = status;
            Int16 GuestMax;
            try
            {
                conx.Open();
                requestcommand.ExecuteNonQuery();


                GuestMax = Convert.ToInt16(cmd.ExecuteScalar());
                
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(GuestMax);
        }

        //[HttpPost]
        //public IHttpActionResult InsertGuest(GuestDto GuestDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    var isExist = _context.Guests.SingleOrDefault(c => c.id == GuestDto.id);
        //    if (isExist != null)
        //        return BadRequest();

        //    var Guest = Mapper.Map<GuestDto, Guest>(GuestDto);

        //    _context.Guests.Add(Guest);
        //    _context.SaveChanges();

        //    GuestDto.id = Guest.id;

           
        //    var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        //    SqlConnection conx = new SqlConnection(connectionString);
        //    SqlCommand cmd = new SqlCommand("select max(id) from guest_tbl", conx);

        //    Int16 GuestMax;
        //    try
        //    {
        //        conx.Open();
        //        GuestMax = Convert.ToInt16(cmd.ExecuteScalar());

        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //    return Ok(GuestMax);
        //}

        [HttpPut]
        //PUT : /api/Building/{id}
        public IHttpActionResult EditGuest(int id, GuestDto GuestDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.Guests.SingleOrDefault(c => c.id != GuestDtos.id);
            if (isExist != null)
                return BadRequest();

            var GuestInDb = _context.Guests.SingleOrDefault(c => c.id == id);
            Mapper.Map(GuestDtos, GuestInDb);
            _context.SaveChanges();

            return Ok(GuestDtos);
        }

        [HttpDelete]
        //PUT : /api/Building/{id}
        public IHttpActionResult DeleteGuest(int id)
        {

            var GuestInDb = _context.Guests.SingleOrDefault(c => c.id == id);
            if (GuestInDb == null)
                return NotFound();
            _context.Guests.Remove(GuestInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
