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
using System.Web;
using Microsoft.AspNet.Identity;
using System.IO;
using System.Data.Entity.Validation;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{

    [Authorize]
    public class OtherExpenseController : ApiController
    {
        private ApplicationDbContext _context;
        public OtherExpenseController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/OtherExpense/{fromdate}/{todate}")]
        public object GetOtherExpenseByDate(DateTime fromdate,DateTime todate)
        {
            var employees = _context.OtherExpenses.Include(c => c.ExpenseTypes).Where(c => c.date >= fromdate).Where(c => c.date <= todate).ToList();
            return Ok(employees);
        }

        [HttpGet]
        public IHttpActionResult GetOtherExpense()
        {
            var employees = _context.OtherExpenses.Include(c => c.ExpenseTypes).ToList();
            return Ok(employees);
        }

        //GET : /api/Employees/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetOtherExpense(int id)
        {
            var employees = _context.OtherExpenses.Include(c => c.ExpenseTypes).SingleOrDefault(c => c.id == id );
            if (employees == null)
                return NotFound();
            return Ok(Mapper.Map<OtherExpense, OtherExpenseDto>(employees));
        }

        //POS : /api/Employees   for Insert record
        [HttpPost]
        public IHttpActionResult CreateOtherExpense()
        {
            var expensetypeid = HttpContext.Current.Request.Form["expensetypeid"];
            var date = HttpContext.Current.Request.Form["date"];
            var amount = HttpContext.Current.Request.Form["amount"];
            var note = HttpContext.Current.Request.Form["note"];
            var createby = User.Identity.GetUserId();
            var createdate = DateTime.Today;
            var photo = HttpContext.Current.Request.Files["image"];

            string photoName = "";
            if (photo != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(photo.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(photo.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(photo.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                photo.SaveAs(fileSavePath);

            }

            var employeeDto = new OtherExpenseDto()
            {
                expensetypeid = Int32.Parse(expensetypeid),
                date = DateTime.Parse(date),
                amount = Decimal.Parse(amount),
                note = note,
                createby = createby,
                createdate = createdate,
                image=photoName,
            };

            var employee = Mapper.Map<OtherExpenseDto, OtherExpense>(employeeDto);
            _context.OtherExpenses.Add(employee);
            _context.SaveChanges();

            employeeDto.id = employee.id;

            return Created(new Uri(Request.RequestUri + "/" + employeeDto.id), employeeDto);
            
        }

        //PUT : /api/Employees/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateOtherExpense(int id)
        {
            var expensetypeid = HttpContext.Current.Request.Form["expensetypeid"];
            var date = HttpContext.Current.Request.Form["date"];
            var amount = HttpContext.Current.Request.Form["amount"];
            var note = HttpContext.Current.Request.Form["note"];
            var createby = User.Identity.GetUserId();
            var createdate = DateTime.Today;
            var photo = HttpContext.Current.Request.Files["image"];
            var old_file = HttpContext.Current.Request.Form["file_old"];

            var empInDb = _context.OtherExpenses.SingleOrDefault(c => c.id == id);

            string photoName = "";
            if (photo != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(photo.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(photo.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(photo.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                photo.SaveAs(fileSavePath);


                //Delete OldPhoto
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.image);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
                var employeeDto = new OtherExpenseDto()
                {
                    id = id,
                    expensetypeid = Int32.Parse(expensetypeid),
                    date = DateTime.Parse(date),
                    amount = Decimal.Parse(amount),
                    note = note,
                    createby = createby,
                    createdate = createdate,
                    image=photoName,
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();
            }else
            {
                var employeeDto = new OtherExpenseDto()
                {
                    id = id,
                    expensetypeid = Int32.Parse(expensetypeid),
                    date = DateTime.Parse(date),
                    amount = Decimal.Parse(amount),
                    note = note,
                    createby = createby,
                    createdate = createdate,
                    image = old_file,
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();
            }

            
            return Ok(new { });

        }
        //DELETE : /api/Employees/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteOtherExpense(int id)
        {
            var empInDb = _context.OtherExpenses.SingleOrDefault(c => c.id == id);
            if (empInDb == null)
                return BadRequest();

            _context.OtherExpenses.Remove(empInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
