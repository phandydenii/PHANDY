using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
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

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class InvoiceDetailController : ApiController
    {
        private ApplicationDbContext _context;
        public InvoiceDetailController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        


        [HttpGet]
        //Get InvoiceDetail by invoiceid
        public IHttpActionResult GetInvoiceDetail(int id)
        {

            var brans = from i in _context.InvoiceDetail
                        join l in _context.Location on i.locationid equals l.id
                        join e in _context.Employee on i.employeeid equals e.Id
                        join p in _context.Product on i.productid equals p.id

                        where (i.invoiceid== id) orderby i.deliverytype ascending

                        select new InvoiceDetailV
                        {
                            id = i.id,
                            invoiceid = i.invoiceid,
                            locationid = i.locationid,
                            location=l.location,
                            productid = i.productid,
                            productname=p.productname,
                            employeeid = i.employeeid,
                            employeename=e.namekh,
                            deliverytype = i.deliverytype,
                            receiverphone = i.receiverphone,
                            paidtype=i.paidtype,
                            price = i.price,
                            pricekh=i.pricekh,
                            carprice = i.carprice,
                            shipprice = i.shipprice,
                            status = i.status,
                            createby = i.createby,
                            createdate = i.createdate,
                            alreadymove=i.alreadymove
                        };

            return Ok(brans);
        }


        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetInvoiceDetail(int id, String b)
        {
            var inv = _context.InvoiceDetail.SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<InvoiceDetail, InvoiceDetailDto>(inv));

        }



        //POS : /api/Payment   for Insert record
        [HttpPost]
        public IHttpActionResult CreateInvoiceDetail(InvoiceDetailDto invDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var department = Mapper.Map<InvoiceDetailDto, InvoiceDetail>(invDetail);
           
            //department.status = true;
            department.createby = User.Identity.GetUserName();
            department.createdate = DateTime.Today;
           
            _context.InvoiceDetail.Add(department);
            _context.SaveChanges();
            invDetail.id = department.id;
            invDetail.alreadymove = false;
            return Created(new Uri(Request.RequestUri + "/" + invDetail.id), invDetail);
            
        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateInvoiceDetail(int id, InvoiceDetail invDetail)
        {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(createdate as DATE) as INVOICEDATE FROM invoicedetail_tbl where id=" + id + "", conx);
            adp.Fill(ds);
            string serverdate = ds.Rows[0][0].ToString();
            string create_date = ds.Rows[0][1].ToString();
            var ParentInDb = _context.InvoiceDetail.SingleOrDefault(c => c.id == id && create_date == serverdate);
            if (ParentInDb == null)
                return BadRequest();


            string UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest();


            var paymentInDb = _context.InvoiceDetail.SingleOrDefault(c => c.id == id);
            paymentInDb.id = invDetail.id;
            paymentInDb.invoiceid = invDetail.invoiceid;
            paymentInDb.locationid = invDetail.locationid;
            paymentInDb.productid = invDetail.productid;
            paymentInDb.employeeid = invDetail.employeeid;
            paymentInDb.deliverytype = invDetail.deliverytype;
            paymentInDb.receiverphone = invDetail.receiverphone;
            paymentInDb.paidtype = invDetail.paidtype;
            paymentInDb.price = invDetail.price;
            paymentInDb.pricekh = invDetail.pricekh;
            paymentInDb.carprice = invDetail.carprice;
            paymentInDb.shipprice = invDetail.shipprice;
            paymentInDb.status = invDetail.status;
            paymentInDb.updateby = User.Identity.GetUserName();
            paymentInDb.updatedate = DateTime.Now;
            paymentInDb.alreadymove = false;
            _context.SaveChanges();
            return Ok();
        }



        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteInvoiceDetail(int id)
        {
            //For Check InvoiceDetail Date 
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(createdate as DATE) as INVOICEDATE FROM invoicedetail_tbl where id=" + id +"", conx);
            adp.Fill(ds);
            string serverdate =   ds.Rows[0][0].ToString();
            string create_date =   ds.Rows[0][1].ToString();
            //string pcdate = DateTime.Now.ToString("yyyy-MM-dd");
            //return Ok(serverdate);

            var ParentInDb = _context.InvoiceDetail.SingleOrDefault(c => c.id == id && create_date == serverdate);
            if (ParentInDb == null)
                return BadRequest();
            
            _context.InvoiceDetail.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
