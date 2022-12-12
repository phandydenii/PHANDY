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
    public class WaterUsageController : ApiController
    {
        private ApplicationDbContext _context;

        public WaterUsageController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/waterusagerecord/{invoiceid}")]
        public IHttpActionResult GetMaxID(int invoiceid)
        {
            ///For Get Max PaymentNo +1
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id,predate,prerecord,currentdate,currentrecord from waterusage_tbl where invoiceid='" + invoiceid + "' order by id desc", conx);
            adp.Fill(ds);
            WaterUsage waterusage = new WaterUsage();
            foreach(DataRow dr in ds.Tables[0].Rows)
            {
                waterusage.id = Convert.ToInt16(dr[0].ToString());
                waterusage.predate = Convert.ToDateTime(dr[1].ToString());
                waterusage.prerecord = Convert.ToDecimal(dr[2].ToString());
                waterusage.currentdate = Convert.ToDateTime(dr[3].ToString());
                waterusage.currentrecord = Convert.ToDecimal(dr[4].ToString());
            }
            return Ok(waterusage);
        }


        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetWaterUsages()
        {
            var getWaterUsage = _context.WaterUsages.ToList().Select(Mapper.Map<WaterUsage, WaterUsageDto>);
            return Ok(getWaterUsage);
        }

        [HttpGet]
        //Get : api/Floors{id}
        public IHttpActionResult GetWaterUsage(int id)
        {
            var getWaterUsageById = _context.WaterUsages.SingleOrDefault(c => c.id == id);

            if (getWaterUsageById == null)
                return NotFound();

            return Ok(Mapper.Map<WaterUsage, WaterUsageDto>(getWaterUsageById));
        }


        [HttpPost]
        public IHttpActionResult CreateWaterUsage()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from waterpowerprice_tbl where IsDeleted=0", conx);
            SqlCommand cmd = new SqlCommand("select max(id) from waterusage_tbl", conx);
            adp.Fill(ds);
            string wpprice = ds.Rows[0][0].ToString();

            var chid = HttpContext.Current.Request.Form["checkinid"];

            var predate = HttpContext.Current.Request.Form["predate"];
            var prerecord = HttpContext.Current.Request.Form["prerecord"];

            var currentdate = HttpContext.Current.Request.Form["currentdate"];
            var currentrecord = HttpContext.Current.Request.Form["currentrecord"];

            SqlCommand command = new SqlCommand();
            SqlCommand requestcommand = new SqlCommand();
            requestcommand.Connection = conx;
            requestcommand.CommandType = CommandType.StoredProcedure;
            requestcommand.CommandText = "INSERT_WATER";
            requestcommand.Parameters.Add("@checkinid", SqlDbType.Int).Value = int.Parse(chid);
            requestcommand.Parameters.Add("@predate", SqlDbType.Date).Value = DateTime.Parse(predate);
            requestcommand.Parameters.Add("@curredate", SqlDbType.Date).Value = DateTime.Parse(currentdate);
            requestcommand.Parameters.Add("@prerecord", SqlDbType.Decimal).Value = decimal.Parse(prerecord);
            requestcommand.Parameters.Add("@currrecord", SqlDbType.Decimal).Value = decimal.Parse(currentrecord);
            requestcommand.Parameters.Add("@price", SqlDbType.Int).Value = int.Parse(wpprice);
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


        [HttpPut]
        [Route("api/updatewaterusage/{checkinid}/{currentrecord}")]
        public IHttpActionResult GetMaxID(int checkinid, decimal currentrecord)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update waterusage_tbl set currentrecord='" + currentrecord + "',currentdate=DATEADD(MONTH,1,predate) where checkinid=" + checkinid, conx);
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
        [Route("api/updatewaters/{checkinid}/{predate}")]
        public IHttpActionResult EditPowerUsage(int checkinid,DateTime predate, WaterUsageDto WaterUsageDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.WaterUsages.SingleOrDefault(c => c.id == WaterUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var WaterUsageInDb = _context.WaterUsages.SingleOrDefault(c => c.checkinid == checkinid && c.predate==predate);
           
            Mapper.Map(WaterUsageDtos, WaterUsageInDb);
            _context.SaveChanges();

            return Ok(WaterUsageDtos);
        }

        [HttpDelete]
        //PUT : /api/Building/{id}
        public IHttpActionResult DeletePowerUsage(int id)
        {

            var BuildingInDb = _context.WaterUsages.SingleOrDefault(c => c.id == id);
            if (BuildingInDb == null)
                return NotFound();
            _context.WaterUsages.Remove(BuildingInDb);
            _context.SaveChanges();

            return Ok(new { });
        }


    }
}
