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
            var GetInvoiceV = (from i in _context.Invoice
                              join id in _context.InvoiceDetail on i.id equals id.invoiceid
                              join ci in _context.Guests on i.checkinid equals ci.id
                              join r in _context.Rooms on id.roomid equals r.id
                              join wu in _context.WaterUsages on i.id equals wu.invoiceid
                              join pu in _context.PowerUsages on i.id equals pu.invoiceid
                              join e in _context.Exchanges on i.exchangerateid equals e.id

                              select new InvoiceDetailV
                              {
                                  id = i.id,
                                  invoiceno=i.invoiceno,
                                  invoicedate=i.invoicedate,
                                  userid=User.Identity.Name,
                                  exchangerate=e.Rate,
                                  grandtotal=i.grandtotal,
                                  totaldollar=i.totaldollar,
                                  totalriel=i.totalriel,
                                  paydollar=id.paydollar,
                                  payriel=id.payriel,
                                  paid=i.paid,
                                  owe=i.owe,
                                  owereassion=i.owereassion,
                                  totalreturnamount=i.totalreturnamount,
                                  returnamount=i.returnamount,
                                  wpredate=wu.predate,
                                  wcurrentdate=wu.currentdate,
                                  wprerecord=wu.prerecord,
                                  wcurrentrecord=wu.currentrecord,
                                  wprice=wu.price,
                                  ppredate=pu.predate,
                                  pcurrentdate=pu.currentdate,
                                  pprerecord=pu.prerecord,
                                  pcurrentrecord=pu.currentrecord,
                                  pprice=pu.price,
                                  roomno = r.room_no,
                                  roomprice = r.price,
                                  checkinid=ci.id
                              }).ToList();
            return Ok(GetInvoiceV);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetInvoiceDetail(int invdetailid)
        {
            var GetInvoiceV = (from i in _context.Invoice
                               join id in _context.InvoiceDetail on i.id equals id.invoiceid
                               join ci in _context.Guests on i.checkinid equals ci.id
                               join r in _context.Rooms on id.roomid equals r.id
                               join wu in _context.WaterUsages on i.id equals wu.invoiceid
                               join pu in _context.PowerUsages on i.id equals pu.invoiceid
                               join e in _context.Exchanges on i.exchangerateid equals e.id
                               where i.id==invdetailid
                               select new InvoiceDetailV
                               {
                                   id = i.id,
                                   invoiceno = i.invoiceno,
                                   invoicedate = i.invoicedate,
                                   userid = User.Identity.Name,
                                   exchangerate = e.Rate,
                                   grandtotal = i.grandtotal,
                                   totaldollar = i.totaldollar,
                                   totalriel = i.totalriel,
                                   paydollar = id.paydollar,
                                   payriel = id.payriel,
                                   paid = i.paid,
                                   owe = i.owe,
                                   owereassion = i.owereassion,
                                   totalreturnamount = i.totalreturnamount,
                                   returnamount = i.returnamount,
                                   wpredate = wu.predate,
                                   wcurrentdate = wu.currentdate,
                                   wprerecord = wu.prerecord,
                                   wcurrentrecord = wu.currentrecord,
                                   wprice = wu.price,
                                   ppredate = pu.predate,
                                   pcurrentdate = pu.currentdate,
                                   pprerecord = pu.prerecord,
                                   pcurrentrecord = pu.currentrecord,
                                   pprice = pu.price,
                                   roomno = r.room_no,
                                   roomprice = r.price,
                                   checkinid=ci.id
                               }).ToList();
            return Ok(GetInvoiceV);
        }
    }
}
