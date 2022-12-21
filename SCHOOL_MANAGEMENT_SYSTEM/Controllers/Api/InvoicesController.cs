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
using System.Web;


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
        //[Route("api/invoices")]
        //Get : api/CheckIns
        public IHttpActionResult GetInvoicessss()
        {
            var getInvoice = _context.Invoice.ToList();
            return Ok(getInvoice);
        }

        [HttpGet]
        [Route("api/invoice-checkind")]
        //Get : api/CheckIns
        public IHttpActionResult GetInvoiceCheckIn()
        {
            var getInvoieCheckIn = (from i in _context.Invoice
                                    join g in _context.Guests on i.guestid equals g.id
                                    join r in _context.Rooms on i.roomid equals r.id
                                    join f in _context.Floors on r.floorid equals f.id
                                    join  b in _context.Buildings on f.buildingid equals b.id
                                    where i.status=="ACTIVE"
                                    select new InvoiceCheckInV
                                    {
                                        invoicedate=i.invoicedate,
                                        paid=i.paid,
                                        printed=i.printed,
                                        roomno=r.room_no,
                                        guestname=g.name,
                                        guestnamekh=g.namekh,
                                        floorno=f.floor_no,
                                        building=b.buildingname
                                    }).ToList();
            return Ok(getInvoieCheckIn);
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
        [Route("api/invoicecheckinmax/{checkinid}/top1")]
        public IHttpActionResult GetInvoiceMax(int checkinid)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1 id,invoicedate,printed,paid from invoice_tbl where checkinid="+checkinid+" order by id desc", conx);
            adp.Fill(ds);
            string id = ds.Rows[0][0].ToString();
            string invoicedate = ds.Rows[0][1].ToString();
            string printed = ds.Rows[0][2].ToString();
            string paid = ds.Rows[0][2].ToString();
            string InvMax = invoicedate + "," +printed+","+paid;
            return Ok(invoicedate);

        }

        [HttpGet]
        [Route("api/invoice_v")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoices()
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join ex in _context.Exchanges on i.exchangerateid equals ex.id
                               join g in _context.Guests on i.guestid equals g.id
                               join r in _context.Rooms on i.roomid equals r.id
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wu in _context.WaterUsages on i.waterusageid equals wu.id
                               join pu in _context.Electrics on i.electricusageid equals pu.id
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id
                               where i.status == "ACTIVE"
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestid=g.id,
                                   guestname=g.name,
                                   guestnamekh=g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   isprint=i.printed,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid = wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate * wp.waterprice,
                                   pid = pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   //ptotal = (pu.currentrecord - pu.prerecord) / ex.Rate * wp.powerprice,
                                   //checkinid = ci.id,
                                   //checkindate = ci.checkindate,
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
                               join g in _context.Guests on i.guestid equals g.id
                               join r in _context.Rooms on i.roomid equals r.id
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wu in _context.WaterUsages on i.waterusageid equals wu.id
                               join el in _context.Electrics on i.electricusageid equals el.id
                               join we in _context.WaterPowerPrices on wu.price equals we.id
                               where i.id==id && i.status == "ACTIVE"
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestid=g.id,
                                   guestname = g.name,
                                   guestnamekh = g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   isprint=i.printed,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid = wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate * we.waterprice,
                                   pid = el.id,
                                   ppredate = el.predate,
                                   pcurrentdate = el.currentdate,
                                   pprerecord = el.prerecord,
                                   pcurrentrecord = el.currentrecord,
                                   pprice = el.price,
                                   ptotal = (el.currentrecord - el.prerecord) / ex.Rate * we.powerprice,
                                   //checkinid = ci.id,
                                   //checkindate = ci.checkindate,
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


        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesNotPrint(int a)
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join ex in _context.Exchanges on i.exchangerateid equals ex.id
                               join g in _context.Guests on i.guestid equals g.id
                               join r in _context.Rooms on i.roomid equals r.id
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wu in _context.WaterUsages on i.waterusageid equals wu.id
                               join eu in _context.Electrics on i.electricusageid equals eu.id
                               join we in _context.WaterPowerPrices on wu.price equals we.id
                               where i.printed == false && i.status == "ACTIVE"
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestid = g.id,
                                   guestname = g.name,
                                   guestnamekh = g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   isprint = i.printed,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid = wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate * we.waterprice,
                                   pid = eu.id,
                                   ppredate = eu.predate,
                                   pcurrentdate = eu.currentdate,
                                   pprerecord = eu.prerecord,
                                   pcurrentrecord = eu.currentrecord,
                                   pprice = eu.price,
                                   ptotal = (eu.currentrecord - eu.prerecord) / ex.Rate * we.powerprice,
                                   //checkinid = ci.id,
                                   //checkindate = ci.checkindate,
                                   roomno = r.room_no,
                                   roomprice = r.price,
                                   roomtypename = rt.roomtypename,
                                   floorno = f.floor_no,
                                   building = b.buildingname,
                                   servicecharge = r.servicecharge,
                                   roomkey = r.roomkey,
                                   roomstatus = r.status

                               }).ToList();
            return Ok(GetInvoiceV);
        }
        [HttpGet]
        [Route("api/invoice_v/{a}/{id}")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesNotPrinIDt(int a,int id)
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join ex in _context.Exchanges on i.exchangerateid equals ex.id
                               join g in _context.Guests on i.guestid equals g.id
                               join r in _context.Rooms on i.roomid equals r.id
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wu in _context.WaterUsages on i.waterusageid equals wu.id
                               join eu in _context.Electrics on i.electricusageid equals eu.id
                               join we in _context.WaterPowerPrices on wu.price equals we.id
                               where i.printed == false && i.id==id && i.status == "ACTIVE"
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestid = g.id,
                                   guestname = g.name,
                                   guestnamekh = g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   isprint = i.printed,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid = wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate * we.waterprice,
                                   pid = eu.id,
                                   ppredate = eu.predate,
                                   pcurrentdate = eu.currentdate,
                                   pprerecord = eu.prerecord,
                                   pcurrentrecord = eu.currentrecord,
                                   pprice = eu.price,
                                   ptotal = (eu.currentrecord - eu.prerecord) / ex.Rate * we.powerprice,
                                   //checkinid = ci.id,
                                   //checkindate = ci.checkindate,
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


        [HttpGet]
        [Route("api/invoice_v/not_paid")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesNotPaid()
        {
           
                var GetInvoiceV = from i in _context.Invoice
                                   join ex in _context.Exchanges on i.exchangerateid equals ex.id
                                   join g in _context.Guests on i.guestid equals g.id
                                   join r in _context.Rooms on i.roomid equals r.id
                                  join e in _context.Exchanges on i.exchangerateid equals e.id
                                   join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                                   join f in _context.Floors on r.floorid equals f.id
                                   join b in _context.Buildings on f.buildingid equals b.id
                                  join wu in _context.WaterUsages on i.waterusageid equals wu.id
                                  join eu in _context.Electrics on i.electricusageid equals eu.id
                                  join we in _context.WaterPowerPrices on wu.price equals we.id
                                  where i.printed == true && i.paid == false && i.status == "ACTIVE"
                                  select new InvoiceV
                                   {
                                       id = i.id,
                                       invoiceno = i.invoiceno,
                                       invoicedate = i.invoicedate,
                                       guestid = g.id,
                                       guestname = g.name,
                                       guestnamekh = g.namekh,
                                       userid = User.Identity.Name,
                                       exchangerate = e.Rate,
                                       grandtotal = i.grandtotal,
                                       totaldollar = i.totaldollar,
                                       totalriel = i.totalriel,
                                       paid = i.paid,
                                       isprint = i.printed,
                                       owe = i.owe,
                                       owereassion = i.owereassion,
                                       totalreturnamount = i.totalreturnamount,
                                       returnamount = i.returnamount,
                                        wid = wu.id,
                                        wpredate = wu.predate,
                                        wcurrentdate = wu.currentdate,
                                        wprerecord = wu.prerecord,
                                        wcurrentrecord = wu.currentrecord,
                                        wprice = wu.price,
                                        wtotal = (wu.currentrecord - wu.prerecord) * we.waterprice / ex.Rate,
                                        pid = eu.id,
                                        ppredate = eu.predate,
                                        pcurrentdate = eu.currentdate,
                                        pprerecord = eu.prerecord,
                                        pcurrentrecord = eu.currentrecord,
                                        pprice = eu.price,
                                        ptotal = (eu.currentrecord - eu.prerecord) * we.powerprice / ex.Rate,
                                      //checkinid = ci.id,
                                      //checkindate = ci.checkindate,
                                      roomno = r.room_no,
                                       roomprice = r.price,
                                       roomtypename = rt.roomtypename,
                                       floorno = f.floor_no,
                                       building = b.buildingname,
                                       servicecharge = r.servicecharge,
                                       roomkey = r.roomkey,
                                       roomstatus = r.status

                                   };
                return Ok(GetInvoiceV);
            
        }

        [HttpGet]
        [Route("api/invoice_v/paid")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesPaid()
        {

            var GetInvoiceV = from i in _context.Invoice
                              join ex in _context.Exchanges on i.exchangerateid equals ex.id
                              join g in _context.Guests on i.guestid equals g.id
                              join r in _context.Rooms on i.roomid equals r.id
                              join e in _context.Exchanges on i.exchangerateid equals e.id
                              join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                              join f in _context.Floors on r.floorid equals f.id
                              join b in _context.Buildings on f.buildingid equals b.id
                              join wu in _context.WaterUsages on i.waterusageid equals wu.id
                              join eu in _context.Electrics on i.electricusageid equals eu.id
                              join we in _context.WaterPowerPrices on wu.price equals we.id
                              where i.printed == true && i.paid == true && i.status == "ACTIVE"
                              select new InvoiceV
                              {
                                  id = i.id,
                                  invoiceno = i.invoiceno,
                                  invoicedate = i.invoicedate,
                                  guestid = g.id,
                                  guestname = g.name,
                                  guestnamekh = g.namekh,
                                  userid = User.Identity.Name,
                                  exchangerate = e.Rate,
                                  grandtotal = i.grandtotal,
                                  totaldollar = i.totaldollar,
                                  totalriel = i.totalriel,
                                  paid = i.paid,
                                  isprint = i.printed,
                                  owe = i.owe,
                                  owereassion = i.owereassion,
                                  totalreturnamount = i.totalreturnamount,
                                  returnamount = i.returnamount,
                                  wid = wu.id,
                                  wpredate = wu.predate,
                                  wcurrentdate = wu.currentdate,
                                  wprerecord = wu.prerecord,
                                  wcurrentrecord = wu.currentrecord,
                                  wprice = wu.price,
                                  wtotal = (wu.currentrecord - wu.prerecord) * we.waterprice / ex.Rate,
                                  pid = eu.id,
                                  ppredate = eu.predate,
                                  pcurrentdate = eu.currentdate,
                                  pprerecord = eu.prerecord,
                                  pcurrentrecord = eu.currentrecord,
                                  pprice = eu.price,
                                  ptotal = (eu.currentrecord - eu.prerecord) * we.powerprice / ex.Rate,
                                  //checkinid = ci.id,
                                  //checkindate = ci.checkindate,
                                  roomno = r.room_no,
                                  roomprice = r.price,
                                  roomtypename = rt.roomtypename,
                                  floorno = f.floor_no,
                                  building = b.buildingname,
                                  servicecharge = r.servicecharge,
                                  roomkey = r.roomkey,
                                  roomstatus = r.status

                              };
            return Ok(GetInvoiceV);

        }
        [HttpGet]
        [Route("api/invoice_v/all")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesPrinIDt()
        {

            var GetInvoiceV = from i in _context.Invoice
                              join ex in _context.Exchanges on i.exchangerateid equals ex.id
                              join g in _context.Guests on i.guestid equals g.id
                              join r in _context.Rooms on i.roomid equals r.id
                              join e in _context.Exchanges on i.exchangerateid equals e.id
                              join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                              join f in _context.Floors on r.floorid equals f.id
                              join b in _context.Buildings on f.buildingid equals b.id
                              join wu in _context.WaterUsages on i.waterusageid equals wu.id
                              join eu in _context.Electrics on i.electricusageid equals eu.id
                              join we in _context.WaterPowerPrices on wu.price equals we.id
                              where i.printed == true && i.status == "ACTIVE"
                              select new InvoiceV
                              {
                                  id = i.id,
                                  invoiceno = i.invoiceno,
                                  invoicedate = i.invoicedate,
                                  guestid = g.id,
                                  guestname = g.name,
                                  guestnamekh = g.namekh,
                                  userid = User.Identity.Name,
                                  exchangerate = e.Rate,
                                  grandtotal = i.grandtotal,
                                  totaldollar = i.totaldollar,
                                  totalriel = i.totalriel,
                                  paid = i.paid,
                                  isprint = i.printed,
                                  owe = i.owe,
                                  owereassion = i.owereassion,
                                  totalreturnamount = i.totalreturnamount,
                                  returnamount = i.returnamount,
                                  wid = wu.id,
                                  wpredate = wu.predate,
                                  wcurrentdate = wu.currentdate,
                                  wprerecord = wu.prerecord,
                                  wcurrentrecord = wu.currentrecord,
                                  wprice = wu.price,
                                  wtotal = (wu.currentrecord - wu.prerecord) * we.waterprice / ex.Rate,
                                  pid = eu.id,
                                  ppredate = eu.predate,
                                  pcurrentdate = eu.currentdate,
                                  pprerecord = eu.prerecord,
                                  pcurrentrecord = eu.currentrecord,
                                  pprice = eu.price,
                                  ptotal = (eu.currentrecord - eu.prerecord) * we.powerprice / ex.Rate,
                                  //checkinid = ci.id,
                                  //checkindate = ci.checkindate,
                                  roomno = r.room_no,
                                  roomprice = r.price,
                                  roomtypename = rt.roomtypename,
                                  floorno = f.floor_no,
                                  building = b.buildingname,
                                  servicecharge = r.servicecharge,
                                  roomkey = r.roomkey,
                                  roomstatus = r.status

                              };
            return Ok(GetInvoiceV);

        }


        [HttpGet]
        [Route("api/invoice_v/1/{id}")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesPrinID(int c,int id)
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join ex in _context.Exchanges on i.exchangerateid equals ex.id
                               join g in _context.Guests on i.guestid equals g.id
                               join r in _context.Rooms on i.roomid equals r.id
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wu in _context.WaterUsages on i.waterusageid equals wu.id
                               join eu in _context.Electrics on i.electricusageid equals eu.id
                               join we in _context.WaterPowerPrices on wu.price equals we.id
                               where i.printed == true && i.id==id && i.status == "ACTIVE"
                               select new InvoiceV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   guestid = g.id,
                                   guestname = g.name,
                                   guestnamekh = g.namekh,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paid = i.paid,
                                   isprint = i.printed,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wid = wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate * we.waterprice,
                                   pid = eu.id,
                                   ppredate = eu.predate,
                                   pcurrentdate = eu.currentdate,
                                   pprerecord = eu.prerecord,
                                   pcurrentrecord = eu.currentrecord,
                                   pprice = eu.price,
                                   ptotal = (eu.currentrecord - eu.prerecord) / ex.Rate * we.powerprice,
                                   //checkinid = ci.id,
                                   //checkindate = ci.checkindate,
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

        [HttpGet]
        [Route("api/invoice-v/newinvoie")]
        //Get : api/Buildings
        public object  GetNewInvoice()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from NewInvoice", conx);
            adp.Fill(ds);
            return ds.Tables[0]; 
        }

        [HttpGet]
        [Route("api/invoice-v/newinvoie/{id}")]
        //Get : api/Buildings
        public object GetNewInvoiceByID(int id)
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();

            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from NewInvoice where NewInvoice='Yes' and guestid="+ id, conx);
            adp.Fill(ds);
            return ds.Tables[0];
        }

        [HttpPost]
        //Get : api/CheckIns
        public IHttpActionResult CreateInvoice(InvoiceDto InvoiceDtos)
        {
            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            DataTable ds2 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlCommand cmd = new SqlCommand("select max(id) from invoice_tbl", conx);

            SqlDataAdapter adp = new SqlDataAdapter("select max(id) from waterusage_tbl", conx);
            SqlDataAdapter adp1 = new SqlDataAdapter("select max(id) from electricusage_tbl", conx);
            SqlDataAdapter adp2 = new SqlDataAdapter("select top 1 id from ExchangeRates where IsDeleted=0 order by id desc", conx);
            adp.Fill(ds);
            adp.Fill(ds1);
            adp2.Fill(ds2);
            string wid = ds.Rows[0][0].ToString();
            string eid = ds1.Rows[0][0].ToString();
            string exid = ds2.Rows[0][0].ToString();


            var InvoiceInDb = Mapper.Map<InvoiceDto, Invoice>(InvoiceDtos);
            InvoiceInDb.invoicedate = DateTime.Today;
            InvoiceInDb.userid = User.Identity.GetUserId();
            InvoiceInDb.createby= User.Identity.GetUserId();
            InvoiceInDb.updateby= User.Identity.GetUserId();
            InvoiceInDb.updatedate= DateTime.Today;
            InvoiceInDb.createdate= DateTime.Today;
            InvoiceInDb.status="ACTIVE";

            InvoiceInDb.exchangerateid = int.Parse(exid);
            InvoiceInDb.electricusageid = int.Parse(eid);
            InvoiceInDb.waterusageid = int.Parse(wid);



            _context.Invoice.Add(InvoiceInDb);
            _context.SaveChanges();
            InvoiceInDb.id = InvoiceDtos.id;

            
            Int16 InvoiceMax;
            try
            {
                conx.Open();
                InvoiceMax = Convert.ToInt16(cmd.ExecuteScalar());

            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok(InvoiceMax);
        }

        [HttpPut]
        [Route("api/invoices/{id}")]
        public IHttpActionResult UpdateUser(int id)
        {
            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            var totalriel = decimal.Parse(HttpContext.Current.Request.Form["totalriel"]);
            var totaldollar = decimal.Parse(HttpContext.Current.Request.Form["totaldollar"]);
            var totalother = decimal.Parse(HttpContext.Current.Request.Form["totalother"]);
            var grandtotal = decimal.Parse(HttpContext.Current.Request.Form["grandtotal"]);
            var payriel = decimal.Parse(HttpContext.Current.Request.Form["payriel"]);
            var paydollar = decimal.Parse(HttpContext.Current.Request.Form["paydollar"]);
            var paid = bool.Parse(HttpContext.Current.Request.Form["paid"]);
            var note = HttpContext.Current.Request.Form["note"];
            
            SqlCommand command = new SqlCommand();
            SqlCommand requestcommand = new SqlCommand();
            requestcommand.Connection = conx;
            requestcommand.CommandType = CommandType.StoredProcedure;
            requestcommand.CommandText = "UPDATE_INVOICE";
            requestcommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            requestcommand.Parameters.Add("@invoicedate", SqlDbType.Date).Value = DateTime.Today;
            requestcommand.Parameters.Add("@grandtotal", SqlDbType.Decimal).Value = grandtotal;
            requestcommand.Parameters.Add("@totaldollar", SqlDbType.Decimal).Value = totaldollar;
            requestcommand.Parameters.Add("@totalriel", SqlDbType.Decimal).Value = totalriel;
            requestcommand.Parameters.Add("@totalother", SqlDbType.Decimal).Value = totalother;
            requestcommand.Parameters.Add("@updateby", SqlDbType.VarChar).Value = User.Identity.GetUserId();
            requestcommand.Parameters.Add("@updatedate", SqlDbType.Date).Value = DateTime.Today;
            requestcommand.Parameters.Add("@note", SqlDbType.NVarChar).Value = note;
            requestcommand.Parameters.Add("@status", SqlDbType.VarChar).Value = "ACTIVE";
            requestcommand.Parameters.Add("@payriel", SqlDbType.Decimal).Value = payriel;
            requestcommand.Parameters.Add("@paydollar", SqlDbType.Decimal).Value = paydollar;
            requestcommand.Parameters.Add("@paid", SqlDbType.Bit).Value = paid;


            try
            {
                conx.Open();
                requestcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok();
        }

        [HttpPut]
        [Route("api/invoices/delete/{id}")]
        public IHttpActionResult DeleteInvoice(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update invoice_tbl set status='DELETE' WHERE id=" + id, conx);
            try
            {
                conx.Open();
                requestcommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return Ok();
        }
    }
}


