using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Web;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class InvoiceDetailsController : ApiController
    {
        private ApplicationDbContext _context;

        public InvoiceDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoiceDetails()
        {
            var GetInvoiceV = _context.InvoiceDetail.Include(w => w.waterusage).Include(e => e.electric).ToList();
            return Ok(GetInvoiceV);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoiceDetail(int id)
        {
            var GetInvoiceV = _context.InvoiceDetail.Include(w => w.waterusage).Include(e => e.electric).Where(c =>c.id==id).SingleOrDefault();
            return Ok(GetInvoiceV);
        }

        [HttpPost]
        public IHttpActionResult InsertStaff()
        {
            var invoiceid = HttpContext.Current.Request.Form["invoiceid"];
            var itemname = HttpContext.Current.Request.Form["itemname"];
            var price = HttpContext.Current.Request.Form["price"];
            var waterusageid = HttpContext.Current.Request.Form["waterusageid"];
            var electricusageid = HttpContext.Current.Request.Form["electricusageid"];
            var paydollar = HttpContext.Current.Request.Form["paydollar"];
            var payriel = HttpContext.Current.Request.Form["payriel"];
       

            var InoiceDetailDto = new InvoiceDetailDto()
            {
                invoiceid=int.Parse(invoiceid),
                itemname=itemname,
                price=decimal.Parse(price),
                waterusageid=int.Parse(waterusageid),
                electricusageid=int.Parse(electricusageid),
                paydollar=decimal.Parse(paydollar),
                payriel=decimal.Parse(payriel),
            };

            var InvoiceDetail = Mapper.Map<InvoiceDetailDto, InvoiceDetail>(InoiceDetailDto);
            _context.InvoiceDetail.Add(InvoiceDetail);
            _context.SaveChanges();
            InoiceDetailDto.id = InvoiceDetail.id;


            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            //SqlConnection con = new SqlConnection(connectionString);            //SqlCommand cmd = new SqlCommand("Select max(id) From PaySlip_V", con);

            //Int16 maxid;
            //try
            //{
            //    con.Open();
            //    cmd.ExecuteNonQuery();
            //    maxid = Convert.ToInt16(cmd.ExecuteScalar());
            //}
            //catch (Exception ex)
            //{
            //    throw ex;
            //}

            return Ok();
        }
     }
}
