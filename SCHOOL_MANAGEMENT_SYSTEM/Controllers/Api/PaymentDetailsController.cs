using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class PaymentDetailsController : ApiController
    {
        private ApplicationDbContext _context;
        public PaymentDetailsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        public IHttpActionResult GetPayment(int id)
        {
            var brans = from pd in _context.PaymentDetails
                        join c in _context.courses on pd.courseid equals c.id
                        where pd.paymentid == id

                        select new PaymentDetailV
                        {
                            id = pd.id,
                            paymentid = pd.paymentid,
                            courseid = pd.courseid,
                            coursename = c.coursename,
                            coursenamekh = c.coursenamekh,
                            qty = pd.qty,
                            turtionfee = pd.turtionfee,
                            discount = pd.discount,
                            total = pd.total
                        };

            return Ok(brans);
        }


        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeletePaymentDetail(int id)
        {
            var ParentInDb = _context.PaymentDetails.SingleOrDefault(c => c.id == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.PaymentDetails.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
