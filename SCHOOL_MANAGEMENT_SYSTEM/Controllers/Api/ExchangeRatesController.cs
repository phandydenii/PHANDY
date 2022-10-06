﻿using AutoMapper;
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
        public IHttpActionResult GetLastExchange(int a,int b)
        {
            var exchageRates = _context.Exchanges.OrderByDescending(c => c.rateid).FirstOrDefault(c => c.IsDeleted == false);
            return Ok(exchageRates);
        }

        [HttpGet]
        public IHttpActionResult Index()
        {
            var exchageRates = _context.Exchanges.Where(c => c.IsDeleted == false).Select(Mapper.Map<ExchangeRate, ExchangeRateDto>).OrderByDescending(c => c.rateid);

            return Ok(exchageRates);
        }
        // GET api/parent/5
        [HttpGet]
        public IHttpActionResult GetExchageRateId(int id)
        {

            var exchageRate = _context.Exchanges.SingleOrDefault(c => c.rateid == id);
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
            DateTime now = DateTime.Now;
            exchageRateDto.date = now;
            var exchageRate = Mapper.Map<ExchangeRateDto, ExchangeRate>(exchageRateDto);
            _context.Exchanges.Add(exchageRate);
            _context.SaveChanges();
            exchageRateDto.rateid = exchageRate.rateid;
            return Created(new Uri(Request.RequestUri + "/" + exchageRate.rateid), exchageRateDto);
        }

        // PUT api/parent/5
        [HttpPut]
        public IHttpActionResult UpdateExchageRate(int id, ExchangeRateDto exchageRateDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var exchageRate = _context.Exchanges.SingleOrDefault(c => c.rateid == id);
            exchageRateDto.date = exchageRate.date;

            Mapper.Map(exchageRateDto, exchageRate);
            _context.SaveChanges();
            return Ok(exchageRateDto);
        }

        // DELETE api/parent/5
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var exchageRate = _context.Exchanges.SingleOrDefault(c => c.rateid == id);
            exchageRate.IsDeleted = true;
            _context.SaveChanges();
            return Ok(new { });
        }
    }
}
