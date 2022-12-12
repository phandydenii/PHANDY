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
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class CheckOutDetailsController : ApiController
    {
        private ApplicationDbContext _context;

        public CheckOutDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [HttpPost]
        public IHttpActionResult CreateCheckOutDetail(CheckOutDetailDto CheckOutDetailDto)
        {
            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            DataTable ds2 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlDataAdapter adp = new SqlDataAdapter("select max(id) from waterusage_tbl", conx);
            SqlDataAdapter adp1 = new SqlDataAdapter("select max(id) from electricusage_tbl", conx);
            SqlDataAdapter adp2 = new SqlDataAdapter("select max(id) from checkout_tbl", conx);
            adp.Fill(ds);
            adp.Fill(ds1);
            adp2.Fill(ds2);

            string wid = ds.Rows[0][0].ToString();
            string eid = ds1.Rows[0][0].ToString();
            string choutid = ds2.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var CheckOutDetailInDb = Mapper.Map<CheckOutDetailDto, CheckOutDeatil>(CheckOutDetailDto);
            CheckOutDetailInDb.waterid = int.Parse(wid);
            CheckOutDetailInDb.electricid = int.Parse(eid);
            CheckOutDetailInDb.checkoutid = int.Parse(choutid);
            _context.CheckOutDeatils.Add(CheckOutDetailInDb);
            _context.SaveChanges();

            CheckOutDetailInDb.id = CheckOutDetailDto.id;

            return Ok(CheckOutDetailInDb);
        }

    }
}
