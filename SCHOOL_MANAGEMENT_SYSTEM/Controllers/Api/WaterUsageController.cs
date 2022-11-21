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
        [Route("api/waterusagerecord/{id}")]
        public IHttpActionResult GetMaxID(int id)
        {
            ///For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id,prerecord,currentrecord from waterusage_tbl where checkinid='" + id + "' order by id desc", conx);
            adp.Fill(ds);
            string oldrecord = ds.Rows[0][1].ToString();
            string newrecord = ds.Rows[0][2].ToString();
            string currenrecrd="";
            if (decimal.Parse(newrecord) == 0)
            {
                currenrecrd = oldrecord;
            }
            else
            {
                currenrecrd = newrecord;
            }
            return Ok(currenrecrd);
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
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.WaterUsages.SingleOrDefault(c => c.id == WaterUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var WaterUsage = Mapper.Map<WaterUsageDto, WaterUsage>(WaterUsageDtos);
            WaterUsage.predate = DateTime.Today;
            _context.WaterUsages.Add(WaterUsage);
            _context.SaveChanges();

            WaterUsageDtos.id = WaterUsage.id;

            return Created(new Uri(Request.RequestUri + "/" + WaterUsageDtos.id), WaterUsageDtos);
        }


        [HttpPut]
        [Route("api/updatewaters/{id}/{currentrecord}")]
        public IHttpActionResult GetMaxID(int id, decimal currentrecord)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update waterusage_tbl set currentrecord='" + currentrecord + "',currentdate=GETDATE() where id=" + id, conx);
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
