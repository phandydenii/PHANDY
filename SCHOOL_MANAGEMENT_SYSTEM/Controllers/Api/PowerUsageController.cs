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
        [Route("api/powerusagerecord/{invoiceid}")]
        public IHttpActionResult GetMaxID(int invoiceid)
        {
            //For Get Max PaymentNo +1
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id,predate,prerecord,currentdate,currentrecord from powerusage_tbl where invoiceid='" + invoiceid + "' order by id desc", conx);
            adp.Fill(ds);
            PowerUsage powerusage = new PowerUsage();
            foreach (DataRow dr in ds.Tables[0].Rows)
            {
                powerusage.id = Convert.ToInt16(dr["id"].ToString());
                powerusage.predate = Convert.ToDateTime(dr["predate"].ToString());
                powerusage.prerecord = Convert.ToDecimal(dr["prerecord"].ToString());
                powerusage.currentdate = Convert.ToDateTime(dr["currentdate"].ToString());
                powerusage.currentrecord = Convert.ToDecimal(dr["currentrecord"].ToString());
            }
            return Ok(powerusage);
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
            DataTable ds1 = new DataTable();
            var connectionString1 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx1 = new SqlConnection(connectionString1);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from waterpowerprice_tbl where IsDeleted=0", conx1);
            adp.Fill(ds1);
            string wpprice = ds1.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.PowerUsages.SingleOrDefault(c => c.id == PowerUsageDtos.id);
            if (isExist != null)
                return BadRequest();

            var PowerUsageInDb = Mapper.Map<PowerUsageDto, PowerUsage>(PowerUsageDtos);
            PowerUsageInDb.predate = DateTime.Today;
            PowerUsageInDb.price = int.Parse(wpprice);
            _context.PowerUsages.Add(PowerUsageInDb);
            _context.SaveChanges();

            PowerUsageDtos.id = PowerUsageInDb.id;

            return Created(new Uri(Request.RequestUri + "/" + PowerUsageDtos.id), PowerUsageDtos);
        }

        [HttpPut]
        [Route("api/updatepowers/{invoiceid}/{currentrecord}")]
        public IHttpActionResult GetMaxID(int invoiceid, decimal currentrecord)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update powerusage_tbl set currentrecord='"+ currentrecord + "',currentdate=DATEADD(MONTH,1,predate) where invoiceid=" + invoiceid, conx);
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


