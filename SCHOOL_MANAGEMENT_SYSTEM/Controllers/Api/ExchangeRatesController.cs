using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Camtopjobs.Controllers.Api
{
    [Authorize]
    [AllowAnonymous]
    public class ExchangeRatesController : ApiController
    {
        private ApplicationDbContext _context;
        public ExchangeRatesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/ExchangeRates/{a}/{b}")]
        public IHttpActionResult GetLastExchange(int a,int b)
        {
            var exchageRates = _context.Exchanges.OrderByDescending(c => c.id).FirstOrDefault(c => c.IsDeleted == false);
            return Ok(exchageRates);
        }

        [HttpGet]
        public IHttpActionResult Index()
        {
            var exchageRates = _context.Exchanges.Where(c => c.IsDeleted == false).Select(Mapper.Map<ExchangeRate, ExchangeRateDto>).OrderByDescending(c => c.id);

            return Ok(exchageRates);
        }
        // GET api/parent/5
        [HttpGet]
        public IHttpActionResult GetExchageid(int id)
        {

            var exchageRate = _context.Exchanges.SingleOrDefault(c => c.id == id);
            if (exchageRate == null)
                return NotFound();

            return base.Ok(Mapper.Map<ExchangeRate, ExchangeRateDto>(exchageRate));
        }

        // POST api/parent
        [HttpPost]
        public IHttpActionResult CreateExchageRate(ExchangeRateDto exchageRateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var exchageRateInDb = Mapper.Map<ExchangeRateDto, ExchangeRate>(exchageRateDto);
            exchageRateInDb.date = DateTime.Today;
            _context.Exchanges.Add(exchageRateInDb);
            _context.SaveChanges();
            exchageRateDto.id = exchageRateInDb.id;
            return Created(new Uri(Request.RequestUri + "/" + exchageRateInDb.id), exchageRateDto);
        }

        // PUT api/parent/5
        [HttpPut]
        public IHttpActionResult UpdateExchageRate(int id, ExchangeRateDto exchageRateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var exchageRateInDb = _context.Exchanges.SingleOrDefault(c => c.id == id);
            exchageRateInDb.date = DateTime.Today;

            Mapper.Map(exchageRateDto, exchageRateInDb);
            _context.SaveChanges();
            return Ok(exchageRateDto);
        }

        // DELETE api/parent/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var exchageRate = _context.Exchanges.SingleOrDefault(c => c.id == id);
            exchageRate.IsDeleted = true;
            _context.SaveChanges();
            return Ok(new { });
        }
    }
}
