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
            var GetInvoiceV = _context.InvoiceDetail.Include(w => w.invoice).ToList();
            return Ok(GetInvoiceV);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoiceDetail(int id)
        {
            var GetInvoiceV = _context.InvoiceDetail.Include(w => w.invoice).Where(c =>c.id==id).SingleOrDefault();
            return Ok(GetInvoiceV);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoiceDetailByCheckIN(int invoiceid)
        {
            var GetInvoiceV = _context.InvoiceDetail.Include(w => w.invoice).Where(c => c.invoiceid == invoiceid).ToList();
            return Ok(GetInvoiceV);
        }

        [HttpPost]
        public IHttpActionResult InsertStaff()
        {
            var invoiceid = HttpContext.Current.Request.Form["invoiceid"];
            var paydollar = HttpContext.Current.Request.Form["paydollar"];
            var payriel = HttpContext.Current.Request.Form["payriel"];

            var checkinid = int.Parse(HttpContext.Current.Request.Form["checkinid"]);
            var fromdate = HttpContext.Current.Request.Form["fromdate"];
            var todate = HttpContext.Current.Request.Form["todate"];


            DataTable dt = new DataTable();
            DataTable dt1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select max(id) from invoice_tbl", conx);
            SqlDataAdapter adp1 = new SqlDataAdapter("select id from paydemage_tbl where checkinid="+ checkinid + " and [date] between '"+ fromdate + "' and '"+ todate +"'", conx);
            adp.Fill(dt);
            adp1.Fill(dt1);

            // string invoiceid = ds.Rows[0][0].ToString();
            //string paydemageid = dt1.Rows[0][0].ToString();
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow row in dt1.Rows)
                {
                    foreach (int id in row.ItemArray)
                    {
                        var paydemageid = _context.PayDemages.Where(p => p.id == id).SingleOrDefault();

                        var InoiceDetailDto = new InvoiceDetailDto()
                        {
                            invoiceid = int.Parse(invoiceid),
                            paydemageid = paydemageid.id,
                            paydollar = decimal.Parse(paydollar),
                            payriel = decimal.Parse(payriel),
                        };

                        var InvoiceDetail = Mapper.Map<InvoiceDetailDto, InvoiceDetail>(InoiceDetailDto);
                        _context.InvoiceDetail.Add(InvoiceDetail);
                        _context.SaveChanges();
                        InoiceDetailDto.id = InvoiceDetail.id;
                    }
                }
            }
            return Ok();
        }
     }
}
