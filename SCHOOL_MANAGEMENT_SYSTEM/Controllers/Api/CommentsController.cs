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
    [Authorize]
    public class CommentsController : ApiController
    {
        private ApplicationDbContext _context;
        public CommentsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpGet]
        //Get InvoiceDetail by invoiceid
        public IHttpActionResult GetComment(int id)
        {
            var comment = _context.Comments.ToList().Select(Mapper.Map<Comment, CommentDto>).Where(c => c.status == true && c.invoiceid==id);
            return Ok(comment);
        }


        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetComment(int id, String b)
        {
            var inv = _context.Comments.SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<Comment, CommentDto>(inv));

        }



        //POS : /api/Payment   for Insert record
        [HttpPost]
        public IHttpActionResult CreateComment(CommentDto invDetail)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var department = Mapper.Map<CommentDto, Comment>(invDetail);

            //department.status = true;
            department.createby = User.Identity.GetUserName();
            department.createdate = DateTime.Today;
            department.status = true;
            _context.Comments.Add(department);
            _context.SaveChanges();
            invDetail.id = department.id;
            return Created(new Uri(Request.RequestUri + "/" + invDetail.id), invDetail);

        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateComment(int id, Comment invDetail)
        {
            //DataTable ds = new DataTable();
            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection conx = new SqlConnection(connectionString);
            //SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(createdate as DATE) as INVOICEDATE FROM invoicedetail_tbl where id=" + id + "", conx);
            //adp.Fill(ds);
            //string serverdate = ds.Rows[0][0].ToString();
            //string create_date = ds.Rows[0][1].ToString();
            //var ParentInDb = _context.InvoiceDetail.SingleOrDefault(c => c.id == id && create_date == serverdate);
            //if (ParentInDb == null)
            //    return BadRequest();


            string UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest();


            var paymentInDb = _context.Comments.SingleOrDefault(c => c.id == id);
            paymentInDb.id = invDetail.id;
            paymentInDb.invoiceid = invDetail.invoiceid;
            paymentInDb.comment = invDetail.comment;
            paymentInDb.status = true;
            paymentInDb.createby = User.Identity.GetUserName();
            paymentInDb.createdate = DateTime.Now;
            _context.SaveChanges();
            return Ok();
        }



        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteComment(int id)
        {
            //For Check InvoiceDetail Date 
            //DataTable ds = new DataTable();
            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection conx = new SqlConnection(connectionString);
            //SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(createdate as DATE) as INVOICEDATE FROM invoicedetail_tbl where id=" + id + "", conx);
            //adp.Fill(ds);
            //string serverdate = ds.Rows[0][0].ToString();
            //string create_date = ds.Rows[0][1].ToString();
            //string pcdate = DateTime.Now.ToString("yyyy-MM-dd");
            //return Ok(serverdate);

            var ParentInDb = _context.Comments.SingleOrDefault(c => c.id == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.Comments.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });

            //var paymentInDb = _context.Comments.SingleOrDefault(c => c.id == id);
            //paymentInDb.status = false;
            //paymentInDb.createby = User.Identity.GetUserName();
            //paymentInDb.createdate = DateTime.Now;
            //_context.SaveChanges();
            //return Ok();


        }
    }
}
