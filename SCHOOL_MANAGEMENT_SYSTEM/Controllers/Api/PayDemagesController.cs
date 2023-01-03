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
            var getBuilding = _context.PayDemages.Include(c => c.guest).ToList();
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuilding(int id)
        {
            var getBuilding = _context.PayDemages.Include(i => i.item).Include(c => c.guest).Where(p => p.id == id).SingleOrDefault();
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuildings(int guestid)
        {
            var getBuilding = _context.PayDemages.Include(i => i.item).Include(c => c.guest).Where(p => p.guestid == guestid).ToList();
            return Ok(getBuilding);
        }

        [HttpGet]
        [Route("api/paydemages/{guestid}/{fromdate}/{todate}")]
        //Get : api/Buildings
        public IHttpActionResult GetBuildings(int guestid, DateTime fromdate, DateTime todate)
        {
            var getBuilding = _context.PayDemages.Include(i => i.item).Include(c => c.guest).Where(p => p.guestid == guestid).Where(p => p.date >= fromdate).Where(p => p.date <= todate).ToList();
            return Ok(getBuilding);
        }


        [HttpPost]
        public IHttpActionResult InsertStaff()
        {
            var guestid = HttpContext.Current.Request.Form["guestid"];
            var itemid = HttpContext.Current.Request.Form["itemid"];
            var price = HttpContext.Current.Request.Form["price"];
            var note = HttpContext.Current.Request.Form["note"];

            var payslipdDto = new PayDemageDto()
            {
               date=DateTime.Today,
               guestid=int.Parse(guestid),
               itemid=int.Parse(itemid),
               price= int.Parse(price),
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
            var guestid = HttpContext.Current.Request.Form["guestid"];
            var itemid = HttpContext.Current.Request.Form["itemid"];
            var price = HttpContext.Current.Request.Form["price"];
            var note = HttpContext.Current.Request.Form["note"];

            var empInDb = _context.PayDemages.SingleOrDefault(c => c.id == id);
            var payslipdDto = new PayDemageDto()
            {
                date = DateTime.Today,
                guestid = int.Parse(guestid),
                itemid = int.Parse(itemid),
                price=decimal.Parse(price),
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
