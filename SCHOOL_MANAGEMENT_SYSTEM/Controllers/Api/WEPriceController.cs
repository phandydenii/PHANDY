using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class WEPriceController : ApiController
    {
        private ApplicationDbContext _context;

        public WEPriceController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        public IHttpActionResult GetLastExchange()
        {
            var exchageRates = _context.WEPrices.OrderByDescending(c => c.id).FirstOrDefault(c => c.IsDeleted == false);
            return Ok(exchageRates);
        }
        [HttpGet]
        public IHttpActionResult GetLastExchange(int id)
        {
            var exchageRates = _context.WEPrices.Where(we => we.id==id).OrderByDescending(c => c.id).FirstOrDefault(c => c.IsDeleted == false);
            return Ok(exchageRates);
        }
        [HttpGet]
        [Route("api/WEPrice/{a}/{b}")]
        public IHttpActionResult GetLastExchange(int a, int b)
        {
            var exchageRates = _context.WEPrices.OrderByDescending(c => c.id).FirstOrDefault(c => c.IsDeleted == false);
            return Ok(exchageRates);
        }


        [HttpPost]
        public IHttpActionResult CreateExchageRate(WEPriceDto wepriceDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var exchageRateInDb = Mapper.Map<WEPriceDto, WEPrice>(wepriceDtos);
            exchageRateInDb.date = DateTime.Today;
            _context.WEPrices.Add(exchageRateInDb);
            _context.SaveChanges();
            wepriceDtos.id = exchageRateInDb.id;
            return Created(new Uri(Request.RequestUri + "/" + exchageRateInDb.id), wepriceDtos);
        }

        // PUT api/parent/5
        [HttpPut]
        public IHttpActionResult UpdateExchageRate(int id, WEPriceDto wepriceDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var exchageRateInDb = _context.WEPrices.SingleOrDefault(c => c.id == id);
            exchageRateInDb.date = DateTime.Today;

            Mapper.Map(wepriceDtos, exchageRateInDb);
            _context.SaveChanges();
            return Ok(wepriceDtos);
        }
    }
}
