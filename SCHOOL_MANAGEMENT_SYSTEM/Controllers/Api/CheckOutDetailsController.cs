using AutoMapper;
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
using System.Web;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class CheckOutDetailsController : ApiController
    {
        private ApplicationDbContext _context;

        public CheckOutDetailsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        [HttpPost]
        public IHttpActionResult CreateCheckOutDetail()
        {
            var checkoutid = HttpContext.Current.Request.Form["checkoutid"];
            var checkinid = int.Parse(HttpContext.Current.Request.Form["checkinid"]);
            var fromdate = HttpContext.Current.Request.Form["fromdate"];
            var todate = HttpContext.Current.Request.Form["todate"];

            DataTable dt = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlDataAdapter adp = new SqlDataAdapter("select id from paydemage_tbl where checkinid=" + checkinid + " and [date] between '" + fromdate + "' and '" + todate + "'", conx);

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    foreach (int id in row.ItemArray)
                    {
                        var paydemageid = _context.PayDemages.Where(p => p.id == id).SingleOrDefault();

                        var InoiceDetailDto = new CheckOutDetailDto()
                        {
                            checkoutid = int.Parse(checkoutid),
                            paydemageid = paydemageid.id,
                        };

                        var InvoiceDetail = Mapper.Map<CheckOutDetailDto, CheckOutDeatil>(InoiceDetailDto);
                        _context.CheckOutDeatils.Add(InvoiceDetail);
                        _context.SaveChanges();
                        InoiceDetailDto.id = InvoiceDetail.id;
                    }
                }
            }
            SqlCommand command = new SqlCommand("update checkin_tbl set active=0 where id="+ checkinid, conx);
            
            try
            {
                conx.Open();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok();
        }

    }
}
