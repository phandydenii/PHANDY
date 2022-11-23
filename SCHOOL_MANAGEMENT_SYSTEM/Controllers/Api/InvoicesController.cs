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
                                    join c in _context.CheckIns on i.checkinid equals c.id
                                    join g in _context.Guests on c.guestid equals g.id
                                    join r in _context.Rooms on c.roomid equals r.id
                                    join f in _context.Floors on r.floorid equals f.id
                                    join  b in _context.Buildings on f.buildingid equals b.id
                                    where i.checkinid==c.id
                                    select new InvoiceCheckInV
                                    {
                                        invoicedate=i.invoicedate,
                                        paid=i.paid,
                                        printed=i.printed,
                                        checkinid=c.id,
                                        checkindate=c.checkindate,
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
                               join ci in _context.CheckIns on i.checkinid equals ci.id
                               join g in _context.Guests on ci.guestid equals g.id
                               join r in _context.Rooms on ci.roomid equals r.id
                               join wu in _context.WaterUsages on ci.id equals wu.checkinid 
                               join pu in _context.PowerUsages on ci.id equals pu.checkinid
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id

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
                                   wid=wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal= (wu.currentrecord - wu.prerecord) / ex.Rate * wp.waterprice,
                                   pid=pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal= (pu.currentrecord - pu.prerecord)/ex.Rate*wp.powerprice,
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
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id
                               where i.id==id
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
                                   wid=wu.id,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate*wp.waterprice,
                                   pid=pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal = (pu.currentrecord - pu.prerecord) /ex.Rate*wp.powerprice,
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


        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesNotPrint(int a)
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
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id
                               where i.printed == false
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
                                   wtotal = (wu.currentrecord - wu.prerecord) /ex.Rate*wp.waterprice,
                                   pid = pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal = (pu.currentrecord - pu.prerecord) /ex.Rate*wp.powerprice,
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
                               join ci in _context.CheckIns on i.checkinid equals ci.id
                               join g in _context.Guests on ci.guestid equals g.id
                               join r in _context.Rooms on ci.roomid equals r.id
                               join wu in _context.WaterUsages on ci.id equals wu.checkinid
                               join pu in _context.PowerUsages on ci.id equals pu.checkinid
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               join rt in _context.RoomTypes on r.roomtypeid equals rt.id
                               join f in _context.Floors on r.floorid equals f.id
                               join b in _context.Buildings on f.buildingid equals b.id
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id
                               where i.printed == false && i.id==id
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
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate*wp.waterprice,
                                   pid = pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal = (pu.currentrecord - pu.prerecord) / ex.Rate*wp.powerprice,
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


        [HttpGet]
        [Route("api/invoice_v/")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesPrinIDt(int c)
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
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id
                               where i.printed == true && i.paid==false
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
                                   wtotal = (wu.currentrecord - wu.prerecord)  * wp.waterprice / ex.Rate,
                                   pid = pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal = (pu.currentrecord - pu.prerecord) * wp.powerprice / ex.Rate,
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

                               }).ToList();
            return Ok(GetInvoiceV);
        }

        [HttpGet]
        [Route("api/invoice_v/1/{id}")]
        //Get : api/Buildings
        public IHttpActionResult GetInvoicesPrinID(int c,int id)
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
                               join wp in _context.WaterPowerPrices on pu.price equals wp.id
                               where i.printed == true && i.id==id
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
                                   wtotal = (wu.currentrecord - wu.prerecord) / ex.Rate*wp.waterprice,
                                   pid = pu.id,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   ptotal = (pu.currentrecord - pu.prerecord) / ex.Rate*wp.powerprice,
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

        [HttpGet]
        [Route("api/invoice-v/newinvoie")]
        //Get : api/Buildings
        public object  GetNewInvoice()
        {
            DataTable dt = new DataTable();
            DataSet ds = new DataSet();
            
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select * from NewInvoice where NewInvoice='Yes'", conx);
            //conx.Open();
            //dr = cmd.ExecuteReader();
            //List<NewInvoiceV> invoicenew = new List<NewInvoiceV>();
            //while (dr.Read())
            //{
            //    NewInvoice = new NewInvoiceV();
            //    NewInvoice.id = dr.GetValue(0).ToString();
            //    NewInvoice.checkindate = dr.GetValue(1).ToString();
            //    NewInvoice.room_no = dr.GetValue(2).ToString();
            //    NewInvoice.roomtypename = dr.GetValue(3).ToString();
            //    NewInvoice.floor_no = dr.GetValue(4).ToString();
            //    NewInvoice.buildingname = dr.GetValue(5).ToString();
            //    NewInvoice.name = dr.GetValue(6).ToString();
            //    NewInvoice.namekh = dr.GetValue(7).ToString();
            //    NewInvoice.sex = dr.GetValue(8).ToString();
            //    NewInvoice.dob = dr.GetValue(9).ToString();
            //    NewInvoice.address = dr.GetValue(10).ToString();
            //    NewInvoice.nationality = dr.GetValue(11).ToString();
            //    NewInvoice.phone = dr.GetValue(12).ToString();
            //    NewInvoice.email = dr.GetValue(13).ToString();
            //    NewInvoice.ssn = dr.GetValue(14).ToString();
            //    NewInvoice.passport = dr.GetValue(15).ToString();
            //    NewInvoice.status = dr.GetValue(16).ToString();
            //    NewInvoice.invoicedate = dr.GetValue(17).ToString();
            //    NewInvoice.paid = dr.GetValue(18).ToString();
            //    NewInvoice.NewInvoice = dr.GetValue(19).ToString();
            //}
            //conx.Close();

            adp.Fill(ds);
            //var NewInvoice = new NewInvoiceV();
            //foreach (DataRow item in ds.Rows)
            //{
            //    NewInvoice.id = item[0].ToString();
            //    NewInvoice.checkindate = item[1].ToString();
            //    NewInvoice.room_no = item[2].ToString();
            //    NewInvoice.roomtypename = item[3].ToString();
            //    NewInvoice.floor_no = item[4].ToString();
            //    NewInvoice.buildingname = item[5].ToString();
            //    NewInvoice.name = item[6].ToString();
            //    NewInvoice.namekh = item[7].ToString();
            //    NewInvoice.sex = item[8].ToString();
            //    NewInvoice.dob = item[9].ToString();
            //    NewInvoice.address = item[10].ToString();
            //    NewInvoice.nationality = item[11].ToString();
            //    NewInvoice.phone = item[12].ToString();
            //    NewInvoice.email = item[13].ToString();
            //    NewInvoice.ssn = item[14].ToString();
            //    NewInvoice.passport = item[15].ToString();
            //    NewInvoice.status = item[16].ToString();
            //    NewInvoice.invoicedate = item[17].ToString();
            //    NewInvoice.paid = item[18].ToString();
            //    NewInvoice.NewInvoice = item[19].ToString();
            //}
            return ds.Tables[0];
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
            InvoiceInDb.status="ACTIVE"; 
                
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
        [Route("api/invoices/{id}")]
        public IHttpActionResult UpdateUser(int id)
        {
            DataTable ds = new DataTable();
            DataTable ds1 = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            var note = HttpContext.Current.Request.Form["note"];

            
            var totalriel = decimal.Parse(HttpContext.Current.Request.Form["totalriel"]);
            var totaldollar = decimal.Parse(HttpContext.Current.Request.Form["totaldollar"]);
            var totalother = decimal.Parse(HttpContext.Current.Request.Form["totalother"]);

            var grandtotal = decimal.Parse(HttpContext.Current.Request.Form["grandtotal"]);


            SqlCommand command = new SqlCommand();
            SqlCommand requestcommand = new SqlCommand();
            requestcommand.Connection = conx;
            requestcommand.CommandType = CommandType.StoredProcedure;
            requestcommand.CommandText = "UPDATE_INVOICE";
            requestcommand.Parameters.Add("@id", SqlDbType.Int).Value = id;
            requestcommand.Parameters.Add("@grandtotal", SqlDbType.Decimal).Value = grandtotal;
            requestcommand.Parameters.Add("@totaldollar", SqlDbType.Decimal).Value = totaldollar;
            requestcommand.Parameters.Add("@totalriel", SqlDbType.Decimal).Value = totalriel;
            requestcommand.Parameters.Add("@totalother", SqlDbType.Decimal).Value = totalother;
            requestcommand.Parameters.Add("@updateby", SqlDbType.VarChar).Value = User.Identity.GetUserId();
            requestcommand.Parameters.Add("@updatedate", SqlDbType.Date).Value = DateTime.Today;
            requestcommand.Parameters.Add("@note", SqlDbType.NVarChar).Value = note;
            requestcommand.Parameters.Add("@status", SqlDbType.VarChar).Value = "Active";


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

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteInvoice(int id)
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);

            SqlCommand requestcommand = new SqlCommand("update invoice_tbl set status='DELETE' WHERE ID=" + id, conx);
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
