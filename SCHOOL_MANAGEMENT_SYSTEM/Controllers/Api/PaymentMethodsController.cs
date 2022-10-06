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

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class PaymentMethodsController : ApiController
    {
        private ApplicationDbContext _context;

        public PaymentMethodsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpGet]
        //Get InvoiceDetail by invoiceid
        public IHttpActionResult GetPaymentMethod()
        {
            var PaymentMethod = _context.PaymentMethods.ToList().Select(Mapper.Map<PaymentMethod, PaymentMethodDto>);
            return Ok(PaymentMethod);
        }


        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetPaymentMethod(int id, string b)
        {
            var inv = _context.PaymentMethods.SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<PaymentMethod, PaymentMethodDto>(inv));

        }

        //POS : /api/Payment   for Insert record
        [HttpPost]
        public IHttpActionResult CreatePaymentMethod(PaymentMethodDto invDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var department = Mapper.Map<PaymentMethodDto, PaymentMethod>(invDetail);

            department.status = true;
            _context.PaymentMethods.Add(department);
            _context.SaveChanges();
            invDetail.id = department.id;
            return Created(new Uri(Request.RequestUri + "/" + invDetail.id), invDetail);

        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdatePaymentMethod(int id, PaymentMethod invDetail)
        {


            string UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest();


            var paymentInDb = _context.PaymentMethods.SingleOrDefault(c => c.id == id);
            paymentInDb.id = invDetail.id;
            paymentInDb.methodname = invDetail.methodname;
            paymentInDb.accountname = invDetail.accountname;
            paymentInDb.accountno = invDetail.accountno;

            paymentInDb.status = true;
            _context.SaveChanges();
            return Ok();
        }



        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeletePaymentMethod(int id)
        {
          

            var ParentInDb = _context.PaymentMethods.SingleOrDefault(c => c.id == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.PaymentMethods.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
