using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class PaymentDeletesController : ApiController
    {
        private ApplicationDbContext _context;
        public PaymentDeletesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //Update Status of Payment
        [HttpPut]
        public IHttpActionResult UpdateGetSalarys(int id, paymentDto paymentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var paymentInDb = _context.Payments.SingleOrDefault(c => c.id == id);
            Mapper.Map(paymentDto, paymentInDb);
            paymentInDb.paymentstatus = "IN ACTIVE";
            paymentInDb.createby = User.Identity.GetUserName();
            paymentInDb.createdate = DateTime.Now;
            _context.SaveChanges();
            return Ok(paymentDto);

        }
    }
}
