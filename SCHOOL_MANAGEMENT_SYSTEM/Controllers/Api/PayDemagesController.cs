using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using AutoMapper;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class PayDemagesController : ApiController
    {
        private ApplicationDbContext _context;

        public PayDemagesController()
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
            var getBuilding = _context.PayDemages.Include(c => c.checkin).ToList();
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuilding(int id)
        {
            var getBuilding = _context.PayDemages.Include(i => i.item).Include(c => c.checkin).Where(p => p.id == id).SingleOrDefault();
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuildings(int checkinid)
        {
            var getBuilding = _context.PayDemages.Include(i => i.item).Include(c => c.checkin).Where(p => p.checkinid == checkinid).ToList();
            return Ok(getBuilding);
        }

        [HttpGet]
        [Route("api/paydemages/{checkinid}/{fromdate}/{todate}")]
        //Get : api/Buildings
        public IHttpActionResult GetBuildings(int checkinid,DateTime fromdate,DateTime todate)
        {
            var getBuilding = _context.PayDemages.Include(i => i.item).Include(c => c.checkin).Where(p => p.checkinid == checkinid).Where(p => p.date >= fromdate).Where(p => p.date<=todate).ToList();
            return Ok(getBuilding);
        }


        [HttpPost]
        public IHttpActionResult InsertStaff()
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select max(id) from checkin_tbl", conx);
            adp.Fill(ds);
           // string checkinid = ds.Rows[0][0].ToString();

            var checkinid = HttpContext.Current.Request.Form["checkinid"];
            var itemid = HttpContext.Current.Request.Form["itemid"];
            var note = HttpContext.Current.Request.Form["note"];

            var payslipdDto = new PayDemageDto()
            {
               date=DateTime.Today,
               checkinid=int.Parse(checkinid),
               itemid=int.Parse(itemid),
               note=note,
            };

            var PaySlip = Mapper.Map<PayDemageDto, PayDemage>(payslipdDto);
            _context.PayDemages.Add(PaySlip);
            _context.SaveChanges();
            payslipdDto.id = PaySlip.id;

            return Ok();
        }

        [HttpPut]
        public IHttpActionResult UpdateStaff(int id)
        {
            var checkinid = HttpContext.Current.Request.Form["checkinid"];
            var itemid = HttpContext.Current.Request.Form["itemid"];
            var note = HttpContext.Current.Request.Form["note"];

            var empInDb = _context.PayDemages.SingleOrDefault(c => c.id == id);
            var payslipdDto = new PayDemageDto()
            {
                date = DateTime.Today,
                checkinid = int.Parse(checkinid),
                itemid = int.Parse(itemid),
                note = note,
            };

            Mapper.Map(payslipdDto, empInDb);
            _context.SaveChanges();
            return Ok();
        }

        [HttpDelete]
        //PUT : /api/Staffs/{id}
        public IHttpActionResult DeleteStaff(int id)
        {
            var StaffInDb = _context.PayDemages.SingleOrDefault(c => c.id == id);
            if (StaffInDb == null)
                return NotFound();
            _context.PayDemages.Remove(StaffInDb);
            _context.SaveChanges();
            return Ok(new { });
        }
    }
}
