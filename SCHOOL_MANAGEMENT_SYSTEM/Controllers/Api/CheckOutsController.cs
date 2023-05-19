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
using System.Web.Http;
using System.Data.Entity;
namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class CheckOutsController : ApiController
    {
        private ApplicationDbContext _context;

        public CheckOutsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetCheckOuts()
        {
            var getBuilding = _context.CheckOuts.Include(g => g.guest).ToList().Select(Mapper.Map<CheckOut, CheckOutDto>);
            return Ok(getBuilding);
        }

        [HttpGet]
        public IHttpActionResult GetCheckOutsById(int id)
        {
            var getBuilding = _context.CheckOuts.Where(c => c.id==id).SingleOrDefault();
            return Ok(getBuilding);
        }

        [HttpGet]
        [Route("api/checkout-v/{id}")]
        //Get : api/Buildings
        public object GetByID(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from CHECK_OUT_V where id=" + id, conx);
            adp.Fill(ds);
            return ds.Tables[0];
        }

        [HttpGet]
        [Route("api/populate-checkin/{checkinid}")]
        //Get : api/Buildings
        public object GetNewInvoiceByID(int checkinid)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from CheckOut_V where checkinid=" + checkinid, conx);
            adp.Fill(ds);
            return ds.Tables[0];
        }

        [HttpPost]
        //Get : api/CheckIns
        public IHttpActionResult CreateCheckOut(CheckOutDto CheckOutDto)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id from ExchangeRates where IsDeleted=0 order by id desc", conx);
            adp.Fill(ds);
            string exid = ds.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var CheckOutInDb = Mapper.Map<CheckOutDto, CheckOut>(CheckOutDto);
            CheckOutInDb.date = DateTime.Today;
            CheckOutInDb.userid = User.Identity.GetUserId();
            CheckOutInDb.exchangeid = int.Parse(exid);

            _context.CheckOuts.Add(CheckOutInDb);
            _context.SaveChanges();

            CheckOutInDb.id = CheckOutDto.id;

            SqlCommand cmd = new SqlCommand("select max(id) from checkout_tbl", conx);
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
        }


        [HttpPut]
        //Get : api/CheckIns
        public IHttpActionResult PutCheckOut(int id,CheckOutDto CheckOutDtos)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id from ExchangeRates where IsDeleted=0 order by id desc", conx);
            adp.Fill(ds);
            string exid = ds.Rows[0][0].ToString();

            var checkOutInDB = _context.CheckOuts.SingleOrDefault(c => c.id == id);
            if (checkOutInDB == null)
                return BadRequest();
            Mapper.Map(CheckOutDtos, checkOutInDB);
            checkOutInDB.userid = User.Identity.GetUserId();
            checkOutInDB.exchangeid = int.Parse(exid);
            _context.SaveChanges();
            return Ok(CheckOutDtos);
        }

        [HttpDelete]
        //PUT : /api/Items/{id}
        public IHttpActionResult DeleteItem(int id)
        {

            var ItemInDb = _context.CheckOuts.SingleOrDefault(c => c.id == id);
            if (ItemInDb == null)
                return NotFound();
            _context.CheckOuts.Remove(ItemInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
