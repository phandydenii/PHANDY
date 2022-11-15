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
    public class PowerUsageController : ApiController
    {
        private ApplicationDbContext _context;

        public PowerUsageController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetPowerUsages()
        {
            var getPowerUsage = _context.PowerUsages.ToList().Select(Mapper.Map<PowerUsage, PowerUsageDto>);
            return Ok(getPowerUsage);
        }

        [HttpGet]
        [Route("api/powerusagerecord/{id}")]
        public IHttpActionResult GetMaxID(int id)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id,prerecord from powerusage_tbl where checkinid='" + id + "' order by id desc", conx);
            adp.Fill(ds);
            string powerrecordid = ds.Rows[0][1].ToString();
            return Ok(powerrecordid);
            //var getPowerUsageById = _context.PowerUsages.SingleOrDefault(c => c.checkinid == id);

            //if (getPowerUsageById == null)
            //    return NotFound();

            //return Ok(Mapper.Map<PowerUsage, PowerUsageDto>(getPowerUsageById));

        }

        [HttpGet]
        //Get : api/Floors{id}
        public IHttpActionResult GetPowerUsage(int id)
        {
            var getPowerUsageById = _context.PowerUsages.SingleOrDefault(c => c.id == id);

            if (getPowerUsageById == null)
                return NotFound();

            return Ok(Mapper.Map<PowerUsage, PowerUsageDto>(getPowerUsageById));
        }


        [HttpPost]
        public IHttpActionResult CreateWaterUsage(PowerUsageDto PowerUsageDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.PowerUsages.SingleOrDefault(c => c.id == PowerUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var WaterUsage = Mapper.Map<PowerUsageDto, PowerUsage>(PowerUsageDtos);
            WaterUsage.predate = DateTime.Today;
            _context.PowerUsages.Add(WaterUsage);
            _context.SaveChanges();

            PowerUsageDtos.id = WaterUsage.id;

            return Created(new Uri(Request.RequestUri + "/" + PowerUsageDtos.id), PowerUsageDtos);
        }

        [HttpPut]
        [Route("api/updatepowers/{checkinid}/{currentrecord}")]
        public IHttpActionResult GetMaxID(int checkinid, decimal currentrecord)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update powerusage_tbl set currentrecord='"+ currentrecord + "',currentdate=GETDATE() where checkinid=" + checkinid, conx);
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
        public IHttpActionResult EditPowerUsage(int id, PowerUsageDto PowerUsageDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.PowerUsages.SingleOrDefault(c => c.id == PowerUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var PowerUsageInDb = _context.PowerUsages.SingleOrDefault(c => c.id == id);
            PowerUsageInDb.predate = DateTime.Today;
            Mapper.Map(PowerUsageDtos, PowerUsageInDb);
            _context.SaveChanges();

            return Ok(PowerUsageDtos);
        }

        [HttpDelete]
        //PUT : /api/Building/{id}
        public IHttpActionResult DeletePowerUsage(int id)
        {

            var BuildingInDb = _context.PowerUsages.SingleOrDefault(c => c.id == id);
            if (BuildingInDb == null)
                return NotFound();
            _context.PowerUsages.Remove(BuildingInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
