using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
        [Authorize]
        public class CollectMoneyController : ApiController
        {
            private ApplicationDbContext _context;
            public CollectMoneyController()
            {
                _context = new ApplicationDbContext();

            }
            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }

            //GET : /api/Employees?departmentid={de..id}  for get all record
            [HttpGet]
            public IHttpActionResult GetCollectMoney(string empId)
            {
            int newempid = int.Parse(empId);

                if (newempid == 0)
                {
                var employees = from c in _context.CollectMoneys//.select(Mapper.Map<Collectmoney, CollectmoneyDto>);
                                join e in _context.Employee on c.employeeid equals e.Id
                                select new CollectMoneyV
                                {
                                    id = c.id,
                                    date = c.date,
                                    employeeid = c.employeeid,
                                    namekh = e.namekh,
                                    amount = c.amount,
                                    deliveryin = c.deliveryin,
                                    deliveryout = c.deliveryout,
                                    bonus = c.bonus,
                                    status = c.status,
                                    createby = c.createby,
                                    createdate = c.createdate
                                };
                return Ok(employees);
                }
                else
                {
                 
                var employees = from c in _context.CollectMoneys//.select(Mapper.Map<Collectmoney, CollectmoneyDto>);
                                join e in _context.Employee on c.employeeid equals e.Id
                                where  c.employeeid == newempid
                                select new CollectMoneyV
                                {
                                    id = c.id,
                                    date = c.date,
                                    employeeid = c.employeeid,
                                    namekh = e.namekh,
                                    amount = c.amount,
                                    deliveryin = c.deliveryin,
                                    deliveryout = c.deliveryout,
                                    bonus = c.bonus,
                                    status = c.status,
                                    createby = c.createby,
                                    createdate = c.createdate
                                };
                return Ok(employees);
            }


            }

            //GET : /api/Employees/{id} for get record by id
            [HttpGet]
            public IHttpActionResult GetCollectMoney(int id)
            {
                var employees = _context.CollectMoneys.SingleOrDefault(c => c.id == id);
                if (employees == null)
                    return NotFound();

                return Ok(Mapper.Map<Collectmoney, CollectmoneyDto>(employees));
            }

        [HttpGet]
        [ResponseType(typeof(Collectmoney))]
        [AllowAnonymous]
        //[Route("CollectMoney/{selectedDate}/{id}")]
        public IHttpActionResult GetCollectMoney(DateTime? selectedDate,int id)
        {
            SqlCommand cmd = new SqlCommand();
            DataTable dt = new DataTable();            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection conx = new SqlConnection(connectionString);            SqlDataAdapter da = new SqlDataAdapter();

            cmd = new SqlCommand("CalculatePaid", conx);
            cmd.Parameters.Add(new SqlParameter("@employeeid", id));
            cmd.Parameters.Add(new SqlParameter("@createdate", selectedDate));
            cmd.CommandType = CommandType.StoredProcedure;
            da.SelectCommand = cmd;
            da.Fill(dt);
            return Ok(dt);
        }



        [HttpGet]
        [ResponseType(typeof(Collectmoney))]
        [AllowAnonymous]
        public IHttpActionResult GetCollectMoneyByDate(DateTime? createdate, int employeeid, string status)
        {
            var brans = from i in _context.InvoiceDetail
                        join l in _context.Location on i.locationid equals l.id
                        join e in _context.Employee on i.employeeid equals e.Id
                        join p in _context.Product on i.productid equals p.id
                        where i.employeeid == employeeid && i.createdate == createdate && i.status == status

                        select new InvoiceDetailV
                        {
                            id = i.id,
                            invoiceid = i.invoiceid,
                            locationid = i.locationid,
                            location = l.location,
                            productid = i.productid,
                            productname = p.productname,
                            employeeid = i.employeeid,
                            employeename = e.namekh,
                            deliverytype = i.deliverytype,
                            receiverphone = i.receiverphone,
                            price = i.price,
                            carprice = i.carprice,
                            shipprice = i.shipprice,
                            status = i.status,
                            createby = i.createby,
                            createdate = i.createdate,
                            alreadymove = i.alreadymove
                        };
            return Ok(brans);
        }



        //POS : /api/Employees   for Insert record
        [HttpPost]
            public IHttpActionResult CreateCollectMoney()
            {
                //var id = HttpContext.Current.Request.Form["Id"];
                var employeeid = HttpContext.Current.Request.Form["employeeid"];
                var date = HttpContext.Current.Request.Form["date"];
                var amount = HttpContext.Current.Request.Form["amount"];
                var deliveryin = HttpContext.Current.Request.Form["deliveryin"];
                var deliveryout = HttpContext.Current.Request.Form["deliveryout"];
                var bonus = HttpContext.Current.Request.Form["bonus"];
                var createby = User.Identity.GetUserName();
                var createdate = DateTime.Today;
                var v1 = HttpContext.Current.Request.Form["status"];
                Boolean b;
                if (v1 == "1")
                {
                    b = true;
                }
                else {
                    b = false;
                }

            var employeeDto = new CollectmoneyDto()
                {
                    //Id = Int32.Parse(id),
                    employeeid = Int32.Parse(employeeid),
                    date = DateTime.Parse(date),
                    amount = Decimal.Parse(amount),
                    deliveryin = Decimal.Parse(deliveryin),
                    deliveryout = Decimal.Parse(deliveryout),
                    bonus = Decimal.Parse(bonus),
                    status= b,
                    createby = createby,
                    createdate = createdate
                };


                try
                {
                    var employee = Mapper.Map<CollectmoneyDto, Collectmoney>(employeeDto);
                    _context.CollectMoneys.Add(employee);
                    _context.SaveChanges();

                    employeeDto.id = employee.id;

                    return Created(new Uri(Request.RequestUri + "/" + employeeDto.id), employeeDto);
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }

            //PUT : /api/Employees/{id}  for Update record
            [HttpPut]
            public IHttpActionResult UpdateCollectMoney(int id)
            {
            //DataTable ds = new DataTable();
            //var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            //SqlConnection conx = new SqlConnection(connectionString);
            //SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(date as DATE) as INVOICEDATE FROM collectmoney_tbl where id=" + id + "", conx);
            //adp.Fill(ds);
            //string serverdate = ds.Rows[0][0].ToString();
            //string collect_date = ds.Rows[0][1].ToString();
            ////string pcdate = DateTime.Now.ToString("yyyy-MM-dd");
            ////return Ok(serverdate);

            //var ParentInDb = _context.Invoice.SingleOrDefault(c => c.id == id && collect_date == serverdate);
            //if (ParentInDb == null)
            //    return BadRequest();
            var ParentInDb = _context.CollectMoneys.SingleOrDefault(c => c.id == id && c.status == false);
            if (ParentInDb == null)
                return BadRequest();

            var employeeid = HttpContext.Current.Request.Form["employeeid"];
            var date = HttpContext.Current.Request.Form["date"];
            var amount = HttpContext.Current.Request.Form["amount"];
            var deliveryin = HttpContext.Current.Request.Form["deliveryin"];
            var deliveryout = HttpContext.Current.Request.Form["deliveryout"];
            var bonus = HttpContext.Current.Request.Form["bonus"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;
            var v1 = HttpContext.Current.Request.Form["status"];
            Boolean b;
            if (v1 == "1")
            {
                b = true;
            }
            else
            {
                b = false;
            }

            var empInDb = _context.CollectMoneys.SingleOrDefault(c => c.id == id);
                var employeeDto = new CollectmoneyDto()
                {
                    employeeid = Int32.Parse(employeeid),
                    date = DateTime.Parse(date),
                    amount = Decimal.Parse(amount),
                    deliveryin = Decimal.Parse(amount),
                    deliveryout = Decimal.Parse(amount),
                    bonus = Decimal.Parse(amount),
                    status = b,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();
                return Ok(new { });

            }
            //DELETE : /api/Employees/{id}  for Delete record
            [HttpDelete]
            public IHttpActionResult DeleteCollectMoney(int id)
            {
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CAST(GETDATE() AS DATE) CURRENTDATE,CAST(date as DATE) as INVOICEDATE FROM collectmoney_tbl where id=" + id + "", conx);
            adp.Fill(ds);
            string serverdate = ds.Rows[0][0].ToString();
            string create_date = ds.Rows[0][1].ToString();
            //string pcdate = DateTime.Now.ToString("yyyy-MM-dd");
            //return Ok(serverdate);

            var ParentInDb = _context.Invoice.SingleOrDefault(c => c.id == id && create_date == serverdate);
            if (ParentInDb == null)
                return BadRequest();

            var paid = _context.CollectMoneys.SingleOrDefault(c => c.status == true && c.id == id );
            if (paid != null)
                return BadRequest();

            var empInDb = _context.CollectMoneys.SingleOrDefault(c => c.id == id);
                if (empInDb == null)
                    return BadRequest();

                _context.CollectMoneys.Remove(empInDb);
                _context.SaveChanges();
                return Ok(new { });


            }
        }

}
