using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{

    public class InvoiceController : ApiController
    {
        private ApplicationDbContext _context;
        public InvoiceController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        public IHttpActionResult GetMaxID(String a, String b)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(invoiceno) IS NULL THEN (1)ELSE MAX(invoiceno)+1 END AS ID,'DE' + RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(invoiceno) IS NULL THEN (1)ELSE MAX(invoiceno)+1 END )) , 6) AS InvoiceID FROM invoice_tbl", conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();
            string InvNoFormat = ds.Rows[0][1].ToString();
            return Ok(InvNo + "," + InvNoFormat);

        }

        [HttpGet]
        public IHttpActionResult GetPaidType(int a, String b,String c)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select top 1(paidtype) from invoicedetail_tbl where invoiceid="+ a +""  , conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();
            return Ok(InvNo);

        }


        [HttpGet]
        [ResponseType(typeof(Invoice))]
        [AllowAnonymous]

        public IHttpActionResult GetInvoice(DateTime? selectedDate)
        {
           
            if (selectedDate == null)
            {
                var brans = from p in _context.Invoice
                            join s in _context.Customer on p.customerid equals s.Id
                            join sf in _context.Showroom on p.showroomid equals sf.id
                            join r in _context.Exchanges on p.exchangeid equals r.rateid
                            where p.status == true 

                            select new InvoiceV
                            {
                                id = p.id,
                                invoiceno = p.invoiceno,
                                date = p.date,
                                customerid = p.customerid,
                                showroomid = p.showroomid,
                                exchangeid=p.exchangeid,
                                rate=r.Rate,
                                showroomname = sf.name,
                                customername = s.name,
                                totalamount = p.totalamount,
                                totalcarprice = p.totalcarprice,
                                totalshipprice = p.totalshipprice,
                                alreadypaid = p.alreadypaid,
                                status = p.status,
                                createby = p.createby,
                                createdate = p.createdate,
                                paid=p.paid,

                            };
                return Ok(brans);
            }
            else {
                var brans = from p in _context.Invoice
                            join s in _context.Customer on p.customerid equals s.Id
                            join sf in _context.Showroom on p.showroomid equals sf.id
                            join r in _context.Exchanges on p.exchangeid equals r.rateid
                            where p.status == true && DbFunctions.TruncateTime(p.date) == selectedDate

                            select new InvoiceV
                                        {
                                            id = p.id,
                                            invoiceno=p.invoiceno,
                                            date = p.date,
                                            customerid = p.customerid,
                                            showroomid = p.showroomid,
                                            showroomname=sf.name,
                                            exchangeid = p.exchangeid,
                                            rate = r.Rate,
                                            customername =s.name,
                                            totalamount = p.totalamount,
                                            totalcarprice = p.totalcarprice,
                                            totalshipprice = p.totalshipprice,
                                            alreadypaid = p.alreadypaid,
                                            status = p.status,
                                            createby = p.createby,
                                            createdate = p.createdate,
                                            paid=p.paid,
                                            
                                        };
                return Ok(brans);
            }
        }


        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetInvoice(int id)
        {
            var inv = _context.Invoice.SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<Invoice, InvoiceDto>(inv));

        }

        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetInvoiceNotPaid(string a)
        {
            DataTable dt = new DataTable();            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter adp = new SqlDataAdapter("select * from invoice_view where id in (select invoiceid from invoicedetail_tbl where deliverytype in(N'កំពុងដឹក',N'ដឹកបន្ត')) and cast(date as date)< cast(getdate() as date) group by id,invoiceno,date,customerid,showroomid,showroomname, customername,totalamount,totalcarprice,totalshipprice,alreadypaid,status,createby,createdate,paid,alreadymove", conx);            adp.Fill(dt);

            //var today = DateTime.Today;
            //var brans = from p in _context.Invoice
            //            join s in _context.Customer on p.customerid equals s.Id
            //            join sf in _context.Showroom on p.showroomid equals sf.id
            //            join pd in _context.InvoiceDetail on p.id equals pd.invoiceid
            //            where p.status == true && pd.deliverytype != "Nជោគជ័យ"
                        
            //            //select * from invoice_tbl where id in (select invoiceid from invoicedetail_tbl where deliverytype<> N'ជោគជ័យ')
            //            select new InvoiceV
            //            {
            //                id = p.id,
            //                invoiceno = p.invoiceno,
            //                date = p.date,
            //                customerid = p.customerid,
            //                showroomid = p.showroomid,
            //                showroomname = sf.name,
            //                customername = s.name,
            //                totalamount = p.totalamount,
            //                totalcarprice = p.totalcarprice,
            //                totalshipprice = p.totalshipprice,
            //                alreadypaid = p.alreadypaid,
            //                status = p.status,
            //                createby = p.createby,
            //                createdate = p.createdate,
            //            } ;

            return Ok(dt);

        }
        //POS : /api/Payment   for Insert record
        [HttpPost]
        public IHttpActionResult CreateInvoice(InvoiceViewModel invoiceViewModel)
        {
            //string UserId = User.Identity.GetUserId();
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(invoiceno) IS NULL THEN (1)ELSE MAX(invoiceno)+1 END AS ID,'DE' + RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(invoiceno) IS NULL THEN (1)ELSE MAX(invoiceno)+1 END )) , 6) AS InvoiceID FROM invoice_tbl", conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();

            var isExists = _context.Invoice.SingleOrDefault(c => c.customerid == invoiceViewModel.customerid && c.date==invoiceViewModel.date);
            if (isExists != null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest();
            Invoice pay = new Invoice()
            {
                date = invoiceViewModel.date,
                invoiceno=Int32.Parse(InvNo),
                customerid = invoiceViewModel.customerid,
                showroomid = invoiceViewModel.showroomid,
                exchangeid=invoiceViewModel.exchangeid,
                totalamount = invoiceViewModel.totalamount,
                totalcarprice = invoiceViewModel.totalcarprice,
                totalshipprice = invoiceViewModel.totalshipprice,
                alreadypaid = invoiceViewModel.alreadypaid,
                status = true,
                createby = User.Identity.GetUserName(),
                createdate = DateTime.Today,
                paid=invoiceViewModel.paid,
                
            };
            _context.Invoice.Add(pay);
            _context.SaveChanges();
            return Ok();
              
        }

        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateInvoice(int id, InvoiceViewModel invoiceViewModel)
        {
            string UserId = User.Identity.GetUserId();
            //bool status = true;
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
            paymentInDb.id = invoiceViewModel.id;
            paymentInDb.invoiceno = invoiceViewModel.invoiceno;
            paymentInDb.date = invoiceViewModel.date;
            paymentInDb.customerid = invoiceViewModel.customerid;
            paymentInDb.showroomid = invoiceViewModel.showroomid;
            paymentInDb.totalamount = invoiceViewModel.totalamount;
            paymentInDb.totalcarprice = invoiceViewModel.totalcarprice;
            paymentInDb.totalshipprice = invoiceViewModel.totalshipprice;
            paymentInDb.alreadypaid = invoiceViewModel.alreadypaid;
            paymentInDb.status = true;
            paymentInDb.createby = User.Identity.GetUserName();
            paymentInDb.createdate = DateTime.Now;
            paymentInDb.paid = invoiceViewModel.paid;
            _context.SaveChanges();
            return Ok();
        }



        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteInvoice(int id)
        {
            
            //For Check InvoiceDetail Date 
            //DataTable ds = new DataTable();
            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection conx = new SqlConnection(connectionString);
            //SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(CAST(GETDATE() AS DATE) as VARCHAR) CURRENTDATE,CAST(createdate as varchar) as CREATEDATE FROM invoice_tbl where id=" + id + "", conx);
            //adp.Fill(ds);
            //string serverdate = ds.Rows[0][0].ToString();
            //string create_date = ds.Rows[0][1].ToString();
            ////string pcdate = DateTime.Now.ToString("yyyy-MM-dd");
            ////return Ok(serverdate);

            //var ParentInDb = _context.Invoice.SingleOrDefault(c => c.id == id && create_date == serverdate);
            //if (ParentInDb == null)
            //    return BadRequest();
            
            //_context.Invoice.Remove(ParentInDb);
            //_context.SaveChanges();
            return Ok(new { });


        }
    }
}
