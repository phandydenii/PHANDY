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
    public class PaymentsController : ApiController
    {
        private ApplicationDbContext _context;
        public PaymentsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetMaxID(String a,String b) {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(PAYMENTNO) IS NULL THEN (1)ELSE MAX(PAYMENTNO)+1 END AS ID,'EIS' + RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(PAYMENTNO) IS NULL THEN (1)ELSE MAX(PAYMENTNO)+1 END )) , 6) AS InvoiceID FROM Payments", conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();
            string InvNoFormat = ds.Rows[0][1].ToString();
            return Ok(InvNo+","+InvNoFormat);

        }


        [HttpGet]
        [ResponseType(typeof(registerstudent))]
        [AllowAnonymous]

        public IHttpActionResult GetPayment()
        {
           
            var brans = from p in _context.Payments
                        join s in _context.Students on p.studentid equals s.id
                        join sf in _context.Shiftes on p.shiftid equals sf.shiftid
                        join g in _context.Grades on p.gradeid equals g.gradeid
                        where p.paymentstatus=="ACTIVE"

                        select new PaymentV 
                        {
                            id = p.id,
                            paymentno = p.paymentno,
                            paymentdate = p.paymentdate,
                            studentid = p.studentid,
                            fullname = s.fullname,
                            fullnamekh=s.fullnamekh,
                            shiftid = p.shiftid,
                            shiftname = sf.shiftname,
                            gradeid = p.gradeid,
                            gradename = g.gradename,
                            enrolldate=p.enrolldate,
                            adminfee=p.adminfee,
                            food=p.food,
                            duration=p.duration,
                            expireddate=p.expireddate,
                            dayextend=p.dayextend,
                            paymentstatus=p.paymentstatus,
                            overdate=p.overdate,
                            note=p.note,
                        };
           
            return Ok(brans);
        }


        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetPayment(int id)
        {
            var parents = _context.Payments.SingleOrDefault(c => c.id == id);
            if (parents == null)
                return NotFound();

            return Ok(Mapper.Map<payment, paymentDto>(parents));

        }


        //POS : /api/Payment   for Insert record
        [HttpPost]
        public IHttpActionResult CreatePaymentAndPaymentDetail(PaymentViewModel paymentViewModel)
        {
            string UserId = User.Identity.GetUserId();
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT ISNULL(Max(paymentno), 0 )+1 AS ID from Payments", conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();
            bool status = true;
            if (!ModelState.IsValid)
                return BadRequest();
            payment pay = new payment()
            {
                paymentno = int.Parse(InvNo),
                paymentdate = paymentViewModel.paymentdate,
                studentid = paymentViewModel.studentid,
                enrolldate = paymentViewModel.enrolldate,
                adminfee = paymentViewModel.adminfee,
                food = paymentViewModel.food,
                duration = paymentViewModel.duration,
                expireddate = paymentViewModel.expireddate,
                dayextend = paymentViewModel.dayextend,
                paymentstatus = paymentViewModel.paymentstatus,
                overdate = paymentViewModel.overdate,
                note = paymentViewModel.note,
                userid = UserId,
                shiftid=paymentViewModel.shiftid,
                gradeid=paymentViewModel.gradeid,
                deposit = paymentViewModel.deposit,
                depositr = paymentViewModel.depositr,
                createby = User.Identity.GetUserName(),
                createdate = DateTime.Now
        };
            _context.Payments.Add(pay);
            var error = _context.GetValidationErrors();
            if (_context.SaveChanges() > 0)
            {
                int MaxPaymentID = _context.Payments.Max(p => p.id);
                foreach (var item in paymentViewModel.Items)
                {
                    paymentdetail paymentDetail = new paymentdetail()
                    {
                        id=item.id,
                        paymentid = MaxPaymentID,
                        courseid = item.courseid,
                        qty = item.qty,
                        turtionfee = item.turtionfee,
                        discount = item.discount,
                        total = item.total
                    };
                    _context.PaymentDetails.Add(paymentDetail);
                }
                if (_context.SaveChanges() > 0)
                {
                    return Ok(status);
                }
            }
            return null;
        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdatePaymentAndPaymentDetail(int id,PaymentViewModel paymentViewModel)
        {
            string UserId = User.Identity.GetUserId();
            //bool status = true;
            if (!ModelState.IsValid)
                return BadRequest();
            var paymentInDb = _context.Payments.SingleOrDefault(c => c.id == id);
            paymentInDb.id = paymentViewModel.id;
            paymentInDb.paymentno = paymentViewModel.paymentno;
                paymentInDb.paymentdate = paymentViewModel.paymentdate;
                paymentInDb.studentid = paymentViewModel.studentid;
                paymentInDb.enrolldate = paymentViewModel.enrolldate;
                paymentInDb.adminfee = paymentViewModel.adminfee;
                paymentInDb.food = paymentViewModel.food;
                paymentInDb.duration = paymentViewModel.duration;
                paymentInDb.expireddate = paymentViewModel.expireddate;
                paymentInDb.dayextend = paymentViewModel.dayextend;
                paymentInDb.paymentstatus = paymentViewModel.paymentstatus;
                paymentInDb.overdate = paymentViewModel.overdate;
                paymentInDb.note = paymentViewModel.note;
                paymentInDb.userid = UserId;
                paymentInDb.shiftid = paymentViewModel.shiftid;
                paymentInDb.gradeid = paymentViewModel.gradeid;
                paymentInDb.deposit = paymentViewModel.deposit;
                paymentInDb.depositr = paymentViewModel.depositr;
                paymentInDb.createby = User.Identity.GetUserName();
                paymentInDb.createdate = DateTime.Now;
            var error = _context.GetValidationErrors();
            if (_context.SaveChanges() > 0){
                //int MaxPaymentID = _context.Payments.Max(p => p.id);
                foreach (var item in paymentViewModel.Items){
                    var detailInDb = _context.PaymentDetails.SingleOrDefault(c => c.id == item.id);
                    if (detailInDb == null)
                    {
                        //Save New
                        paymentdetail paymentDetail = new paymentdetail()
                        {
                            id = item.id,
                            paymentid = paymentInDb.id,
                            courseid = item.courseid,
                            qty = item.qty,
                            turtionfee = item.turtionfee,
                            discount = item.discount,
                            total = item.total
                        };
                        _context.PaymentDetails.Add(paymentDetail);
                    }
                    else {
                        //For Update
                        detailInDb.id = item.id;
                        detailInDb.paymentid = item.paymentid;
                        detailInDb.courseid = item.courseid;
                        detailInDb.qty = item.qty;
                        detailInDb.turtionfee = item.turtionfee;
                        detailInDb.discount = item.discount;
                        detailInDb.total = item.total;
                    }
                }
                _context.SaveChanges();
                return Ok();
                
            }
            return null;
        }

        

        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeletePayment(int id)
        {
            var ParentInDb = _context.Payments.SingleOrDefault(c => c.id == id);
            if (ParentInDb == null)
                return BadRequest();

            

            _context.Payments.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
