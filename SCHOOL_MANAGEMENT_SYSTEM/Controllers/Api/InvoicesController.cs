using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class InvoicesController : ApiController
    {
        private ApplicationDbContext _context;

        public InvoicesController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/invoicemaxid")]
        public IHttpActionResult GetInvoiceMaxID()
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(id) IS NULL THEN (1)ELSE MAX(id)+1 END AS ID,'RL'+ RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(id) IS NULL THEN (1)ELSE MAX(id)+1 END )) , 6) AS BookingNo FROM invoice_tbl", conx);
            adp.Fill(ds);
            string BookingNo = ds.Rows[0][1].ToString();
            return Ok(BookingNo);

        }

        [HttpGet]
        [Route("api/invoice_v")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoices()
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join ex in _context.Exchanges on i.exchangerateid equals ex.id
                               join ci in _context.CheckIns on i.checkinid equals ci.id
                               join g in _context.Guests on ci.guestid equals g.id
                               join r in _context.Rooms on ci.roomid equals r.id
                               join wu in _context.WaterUsages on ci.id equals wu.checkinid 
                               join pu in _context.PowerUsages on ci.id equals pu.checkinid
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               where i.paid==false
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestname=g.name,
                                   guestnamekh=g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid=wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal= (wu.currentrecord - wu.prerecord) * ex.Rate,
                                   pid=pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal= (pu.currentrecord - pu.prerecord)*ex.Rate,
                                   checkinid = ci.id,
                                   checkindate=ci.checkindate,
                                   roomno = r.room_no,
                                   roomprice = r.price,
                                   roomtypename=rt.roomtypename,
                                   floorno=f.floor_no,
                                   building=b.buildingname,
                                   servicecharge=r.servicecharge,
                                   roomkey=r.roomkey,
                                   roomstatus=r.status

                               }).ToList();
            return Ok(GetInvoiceV);
        }

        [HttpGet]
        [Route("api/invoice_v/{id}")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoice(int id)
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join ex in _context.Exchanges on i.exchangerateid equals ex.id
                               join ci in _context.CheckIns on i.checkinid equals ci.id
                               join g in _context.Guests on ci.guestid equals g.id
                               join r in _context.Rooms on ci.roomid equals r.id
                               join wu in _context.WaterUsages on ci.id equals wu.checkinid
                               join pu in _context.PowerUsages on ci.id equals pu.checkinid
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               where i.paid == false && i.id==id
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestname = g.name,
                                   guestnamekh = g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid=wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) * ex.Rate,
                                   pid=pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal = (pu.currentrecord - pu.prerecord) * ex.Rate,
                                   checkinid = ci.id,
                                   checkindate = ci.checkindate,
                                   roomno = r.room_no,
                                   roomprice = r.price,
                                   roomtypename = rt.roomtypename,
                                   floorno = f.floor_no,
                                   building = b.buildingname,
                                   servicecharge = r.servicecharge,
                                   roomkey = r.roomkey,
                                   roomstatus = r.status

                               }).SingleOrDefault();
            return Ok(GetInvoiceV);
        }

        [HttpPost]
        //Get : api/CheckIns
        public IHttpActionResult CreateInvoice(InvoiceDto InvoiceDtos)
        {
            DataTable ds1 = new DataTable();
            var connectionString1 = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx1 = new SqlConnection(connectionString1);
            SqlDataAdapter adp = new SqlDataAdapter("select max(id) as CheckInMaxID from checkin_tbl", conx1);
            adp.Fill(ds1);
            string CheckInId = ds1.Rows[0][0].ToString();

            if (!ModelState.IsValid)
                return BadRequest();

            var InvoiceInDb = Mapper.Map<InvoiceDto, Invoice>(InvoiceDtos);
            InvoiceInDb.invoicedate = DateTime.Today;
            InvoiceInDb.checkinid = Int16.Parse(CheckInId);
            InvoiceInDb.userid = User.Identity.GetUserId();
            InvoiceInDb.createby= User.Identity.GetUserId();
            InvoiceInDb.updateby= User.Identity.GetUserId();
            InvoiceInDb.updatedate= DateTime.Today;
            InvoiceInDb.createdate= DateTime.Today;

            _context.Invoice.Add(InvoiceInDb);
            _context.SaveChanges();

            InvoiceInDb.id = InvoiceDtos.id;

            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select max(id) from invoice_tbl", conx);
            Int16 InvoiceMax;
            try
            {
                conx.Open();
                cmd.ExecuteNonQuery();
                InvoiceMax = Convert.ToInt16(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(InvoiceMax);
        }

        [HttpPut]
        public IHttpActionResult UpdateUser(int id, InvoiceDto InvoiceDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var CheckIninDb = _context.Invoice.SingleOrDefault(c => c.id == id);
            Mapper.Map(InvoiceDto, CheckIninDb);
            _context.SaveChanges();

            return Ok(CheckIninDb);
        }

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteUser(int id)
        {
            var bookinfInDb = _context.Invoice.SingleOrDefault(c => c.id == id);
            if (bookinfInDb == null)
                return NotFound();
            _context.Invoice.Remove(bookinfInDb);
            _context.SaveChanges();

            return Ok(new { });
        }



    }

    

}
