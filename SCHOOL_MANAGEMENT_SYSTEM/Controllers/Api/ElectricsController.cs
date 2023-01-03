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
    public class ElectricsController : ApiController
    {
        private ApplicationDbContext _context;

        public ElectricsController()
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
            var getPowerUsage = _context.Electrics.ToList().Select(Mapper.Map<ElectricUsage, ElectricUsageDto>);
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
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id,predate,prerecord,currentdate,currentrecord from electricusage_tbl where invoiceid='" + invoiceid + "' order by id desc", conx);
            adp.Fill(ds);
            Models.ElectricUsage powerusage = new Models.ElectricUsage();
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
            var getPowerUsageById = _context.Electrics.SingleOrDefault(c => c.id == id);

            if (getPowerUsageById == null)
                return NotFound();

            return Ok(Mapper.Map<ElectricUsage, ElectricUsageDto>(getPowerUsageById));
        }


        [HttpPost]
        public IHttpActionResult INSERT_ELECTRIC()
        {
            DataTable ds1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from weprice_tbl where IsDeleted=0", conx);
            SqlCommand cmd = new SqlCommand("select max(id) from electricusage_tbl", conx);
            adp.Fill(ds1);
            string wpprice = ds1.Rows[0][0].ToString();

            var chid = HttpContext.Current.Request.Form["checkinid"];
            var predate = HttpContext.Current.Request.Form["predate"];
            var prerecord = HttpContext.Current.Request.Form["prerecord"];
            var currentdate = HttpContext.Current.Request.Form["currentdate"];
            var currentrecord = HttpContext.Current.Request.Form["currentrecord"];

            var EletricusageDto = new ElectricUsageDto()
            {
                checkinid = int.Parse(chid),
                predate = DateTime.Today,
                prerecord = decimal.Parse(prerecord),
                currentdate = DateTime.Today,
                currentrecord = decimal.Parse(currentrecord),
                price = int.Parse(wpprice),
            };

            var ElectricUsage = Mapper.Map<ElectricUsageDto, ElectricUsage>(EletricusageDto);
            _context.Electrics.Add(ElectricUsage);
            _context.SaveChanges();
            EletricusageDto.id = ElectricUsage.id;

            Int16 GuestMax;
            try
            {
                conx.Open();
                GuestMax = Convert.ToInt16(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(GuestMax);
        }

        [HttpPut]
        [Route("api/updateelectrics/{checkinid}/{currentrecord}")]
        public IHttpActionResult GetMaxID(int checkinid, decimal currentrecord)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update electricusage_tbl set currentrecord='" + currentrecord + "',currentdate=DATEADD(MONTH,1,predate) where checkinid=" + checkinid, conx);
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
        [Route("api/updatepowers/{checkinid}/{predate}")]
        public IHttpActionResult EditPowerUsage(int checkinid,DateTime predate, ElectricUsageDto ElectricUsageDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var ElectricUsageInDb = _context.Electrics.SingleOrDefault(c => c.checkinid == checkinid && c.predate==predate);
         
            Mapper.Map(ElectricUsageDtos, ElectricUsageInDb);
            _context.SaveChanges();

            return Ok(ElectricUsageDtos);
        }

        [HttpDelete]
        //PUT : /api/Building/{id}
        public IHttpActionResult DeletePowerUsage(int id)
        {

            var ElectricUsageInDb = _context.Electrics.SingleOrDefault(c => c.id == id);
            if (ElectricUsageInDb == null)
                return NotFound();
            _context.Electrics.Remove(ElectricUsageInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}


