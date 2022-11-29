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
        public IHttpActionResult CreateWaterUsage(WaterUsageDto WaterUsageDtos)
        {
            DataTable ds1 = new DataTable();
            var connectionString1 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx1 = new SqlConnection(connectionString1);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from waterpowerprice_tbl where IsDeleted=0", conx1);
            adp.Fill(ds1);
            string wpprice = ds1.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.WaterUsages.SingleOrDefault(c => c.id == WaterUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var WaterUsageInDb = Mapper.Map<WaterUsageDto, WaterUsage>(WaterUsageDtos);
            WaterUsageInDb.predate = DateTime.Today;
            WaterUsageInDb.price = int.Parse(wpprice);
            _context.WaterUsages.Add(WaterUsageInDb);
            _context.SaveChanges();

            WaterUsageDtos.id = WaterUsageInDb.id;

            return Created(new Uri(Request.RequestUri + "/" + WaterUsageDtos.id), WaterUsageDtos);
        }


        [HttpPut]
        [Route("api/updatewaters/{invoiceid}/{currentrecord}")]
        public IHttpActionResult GetMaxID(int invoiceid, decimal currentrecord)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update waterusage_tbl set currentrecord='" + currentrecord + "',currentdate=DATEADD(MONTH,1,predate) where invoiceid=" + invoiceid, conx);
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
        //PUT : /api/Building/{id}
        public IHttpActionResult EditPowerUsage(int id, WaterUsageDto WaterUsageDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.WaterUsages.SingleOrDefault(c => c.id == WaterUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var WaterUsageInDb = _context.WaterUsages.SingleOrDefault(c => c.id == id);
            WaterUsageInDb.predate = DateTime.Today;
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
