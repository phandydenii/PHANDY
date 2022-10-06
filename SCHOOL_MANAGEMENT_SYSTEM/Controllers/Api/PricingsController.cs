using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class PricingsController : ApiController
    {
        private ApplicationDbContext _context;

        public PricingsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpGet]
        //Get InvoiceDetail by invoiceid
        public IHttpActionResult GetPricing()
        {
            var Pricing = _context.Pricings.Include(c => c.employee).ToList().Select(Mapper.Map<Pricing, PricingDto>);
            return Ok(Pricing);
        }


        //GET : /api/Pricings/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetPricing(int id)
        {
            var inv = _context.Pricings.Include(c => c.employee).SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<Pricing, PricingDto>(inv));

        }

        //POS : /api/Pricing   for Insert record
        [HttpPost]
        public IHttpActionResult CreatePricing(PricingDto PricingDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var PricingInDb = Mapper.Map<PricingDto, Pricing>(PricingDto);
            PricingInDb.descrition = PricingDto.descrition;
            PricingInDb.pricing = PricingDto.pricing;
            PricingInDb.createby = User.Identity.GetUserName();
            PricingInDb.updatedate = DateTime.Today;
            PricingInDb.updateby = User.Identity.GetUserName();
            PricingInDb.createdate = DateTime.Today;
            _context.Pricings.Add(PricingInDb);
            _context.SaveChanges();
            PricingDto.id = PricingInDb.id;
            return Created(new Uri(Request.RequestUri + "/" + PricingDto.id), PricingDto);

        }

        //PUT : /api/Pricings/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdatePricing(int id, Pricing Pricing)
        {


            string UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest();


            var PricingInDb = _context.Pricings.SingleOrDefault(c => c.id == id);
            PricingInDb.id = Pricing.id;
            PricingInDb.descrition = Pricing.descrition;
            PricingInDb.employeeid = Pricing.employeeid;
            PricingInDb.date = Pricing.date;
            PricingInDb.pricing = Pricing.pricing;
            PricingInDb.createby = User.Identity.GetUserName();
            PricingInDb.updatedate = DateTime.Today;
            PricingInDb.updateby = User.Identity.GetUserName();
            PricingInDb.createdate = DateTime.Today;
            _context.SaveChanges();
            return Ok(Pricing);
        }



        //DELETE : /api/Pricings/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeletePricing(int id)
        {


            var PricingInDb = _context.Pricings.SingleOrDefault(c => c.id == id);
            if (PricingInDb == null)
                return BadRequest();

            _context.Pricings.Remove(PricingInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
