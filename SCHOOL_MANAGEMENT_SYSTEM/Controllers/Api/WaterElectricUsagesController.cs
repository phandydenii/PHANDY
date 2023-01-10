using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using AutoMapper;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class WaterElectricUsagesController : ApiController
    {
        private ApplicationDbContext _context;

        public WaterElectricUsagesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuilding()
        {
            var getBuilding = _context.WaterEletricUsages.Include(s => s.checkin).Include(w =>w.weprice).ToList();
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuilding(int id)
        {
            var getBuilding = _context.WaterEletricUsages.Include(s => s.checkin).Include(w => w.weprice).Where(w =>w.id==id).ToList();
            return Ok(getBuilding);
        }

        [HttpPost]
        public IHttpActionResult InsertWEU( )
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from weprice_tbl where IsDeleted=0", conx);
            adp.Fill(ds);
            string weprice = ds.Rows[0][0].ToString();



            //var WEInDb = Mapper.Map<WaterElectricUsageDto, WaterElectricUsage>(WaterElectricUsageDtos);
            //WEInDb.wepriceid = int.Parse(weprice);
            //_context.WaterEletricUsages.Add(WEInDb);
            //_context.SaveChanges();

            var checkinid = HttpContext.Current.Request.Form["checkinid"];
            var startdate = HttpContext.Current.Request.Form["startdate"];
            var enddate = HttpContext.Current.Request.Form["enddate"];
            var wstartrecord = HttpContext.Current.Request.Form["wstartrecord"];
            var wendrecord = HttpContext.Current.Request.Form["wendrecord"];
            var estartrecord = HttpContext.Current.Request.Form["estartrecord"];
            var eendrecord = HttpContext.Current.Request.Form["eendrecord"];
            SqlCommand command = new SqlCommand();
            SqlCommand requestcommand = new SqlCommand();
            requestcommand.Connection = conx;
            requestcommand.CommandType = CommandType.StoredProcedure;
            requestcommand.CommandText = "InsertWaterElectric";
            requestcommand.Parameters.Add("@checkinid", SqlDbType.Int).Value = int.Parse(checkinid);
            requestcommand.Parameters.Add("@startdate", SqlDbType.Date).Value = DateTime.Parse(startdate);
            requestcommand.Parameters.Add("@enddate", SqlDbType.Date).Value = DateTime.Parse(enddate);
            requestcommand.Parameters.Add("@wstartrecord", SqlDbType.Decimal).Value = decimal.Parse(wstartrecord);
            requestcommand.Parameters.Add("@wendrecord", SqlDbType.Decimal).Value = decimal.Parse(wendrecord);
            requestcommand.Parameters.Add("@estartrecord", SqlDbType.Decimal).Value = decimal.Parse(estartrecord);
            requestcommand.Parameters.Add("@eendrecord", SqlDbType.Decimal).Value = decimal.Parse(eendrecord);
            requestcommand.Parameters.Add("@weprice", SqlDbType.Int).Value = int.Parse(weprice);
            try
            {
                conx.Open();
                requestcommand.ExecuteNonQuery();
                conx.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            SqlCommand cmd = new SqlCommand("Select max(id) From waterelectricusage_tbl", conx);
            Int16 maxid;
            try
            {
                conx.Open();
                cmd.ExecuteNonQuery();
                maxid = Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(maxid);
        }

        [HttpPut]
        public IHttpActionResult UpdateStaff(int id)
        {

            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select Max(id) from weprice_tbl where IsDeleted=0", conx);
            adp.Fill(ds);
            string weprice = ds.Rows[0][0].ToString();

            var checkinid = HttpContext.Current.Request.Form["checkinid"];
            var startdate = HttpContext.Current.Request.Form["startdate"];
            var enddate = HttpContext.Current.Request.Form["enddate"];
            var wstartrecord = HttpContext.Current.Request.Form["wstartrecord"];
            var wendrecord = HttpContext.Current.Request.Form["wendrecord"];
            var estartrecord = HttpContext.Current.Request.Form["estartrecord"];
            var eendrecord = HttpContext.Current.Request.Form["eendrecord"];

            var empInDb = _context.WaterEletricUsages.SingleOrDefault(c => c.id == id);
            var waterElectricUsageDto = new WaterElectricUsageDto()
            {
                checkinid = int.Parse(checkinid),
                startdate = DateTime.Parse(startdate),
                enddate = DateTime.Parse(enddate),
                wstartrecord = decimal.Parse(wstartrecord),
                wendrecord = decimal.Parse(wendrecord),
                estartrecord = decimal.Parse(estartrecord),
                eendrecord = decimal.Parse(eendrecord),
                wepriceid = int.Parse(weprice),

            };

            Mapper.Map(waterElectricUsageDto, empInDb);
            _context.SaveChanges();
            return Ok();
        }
    }
}

