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
            var getBuilding = _context.CheckOuts.ToList().Select(Mapper.Map<CheckOut, CheckOutDto>);
            return Ok(getBuilding);
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
            DataTable ds1 = new DataTable();
            DataTable ds2 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id from ExchangeRates where IsDeleted=0 order by id desc", conx);
            SqlDataAdapter adp1 = new SqlDataAdapter("select max(id) from waterusage_tbl", conx);
            SqlDataAdapter adp2 = new SqlDataAdapter("select max(id) from electricusage_tbl", conx);

            adp.Fill(ds);
            adp1.Fill(ds1);
            adp2.Fill(ds2);
            string exid = ds.Rows[0][0].ToString();
            string wid = ds1.Rows[0][0].ToString();
            string eid = ds2.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var CheckOutInDb = Mapper.Map<CheckOutDto, CheckOut>(CheckOutDto);
            CheckOutInDb.date = DateTime.Today;
            CheckOutInDb.userid = User.Identity.GetUserName();
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
    }
}
