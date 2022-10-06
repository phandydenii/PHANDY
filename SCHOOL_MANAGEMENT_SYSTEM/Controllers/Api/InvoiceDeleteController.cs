using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
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
    public class InvoiceDeleteController : ApiController
    {
        private ApplicationDbContext _context;
        public InvoiceDeleteController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //Update Status of Payment
        [HttpPut]
        public IHttpActionResult UpdateInvoice(int id, InvoiceDto invoiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(date as DATE) as INVOICEDATE FROM invoice_tbl where id=" + id + "", conx);
            adp.Fill(ds);
            string serverdate = ds.Rows[0][0].ToString();
            string create_date = ds.Rows[0][1].ToString();
            //string pcdate = DateTime.Now.ToString("yyyy-MM-dd");
            //return Ok(serverdate);

            var ParentInDb = _context.Invoice.SingleOrDefault(c => c.id == id && create_date == serverdate);
            if (ParentInDb == null)
                return BadRequest();

            var paymentInDb = _context.Invoice.SingleOrDefault(c => c.id == id);
            Mapper.Map(invoiceDto, paymentInDb);
            paymentInDb.status = false;
            paymentInDb.createby = User.Identity.GetUserName();
            paymentInDb.createdate = DateTime.Now;
            _context.SaveChanges();
            return Ok(invoiceDto);

        }
    }
}
