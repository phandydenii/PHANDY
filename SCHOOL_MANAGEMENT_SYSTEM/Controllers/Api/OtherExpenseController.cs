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

        //GET : /api/Employees?departmentid={de..id}  for get all record
        [HttpGet]
        public IHttpActionResult GetOtherExpense(string showroomId)
        {

            if (showroomId == "all")
            {
                var employees = _context.OtherExpenses.Include(c => c.Showrooms).ToList()
                                                        .Select(Mapper.Map<OtherExpense, OtherExpenseDto>);

                return Ok(employees);
            }
            else
            {
                var employees = _context.OtherExpenses
                                            .Include(c => c.Showrooms).Select(Mapper.Map<OtherExpense, OtherExpenseDto>)
                                            .Where(c => c.showroomid == int.Parse(showroomId));
                return Ok(employees);
            }
            //var employees = _context.Employees.ToList().Select(Mapper.Map<Employee, EmployeeDto>);
            //return Ok(employees);


        }

        //GET : /api/Employees/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetOtherExpense(int id)
        {
            var employees = _context.OtherExpenses.Include(c => c.Showrooms).SingleOrDefault(c => c.id == id );
            if (employees == null)
                return NotFound();

            return Ok(Mapper.Map<OtherExpense, OtherExpenseDto>(employees));
        }

        //POS : /api/Employees   for Insert record
        [HttpPost]
        public IHttpActionResult CreateOtherExpense()
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var expensetypeid = HttpContext.Current.Request.Form["expensetypeid"];
            var date = HttpContext.Current.Request.Form["date"];
            var amount = HttpContext.Current.Request.Form["amount"];
            var note = HttpContext.Current.Request.Form["note"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;

            var employeeDto = new OtherExpenseDto()
            {
                //Id = Int32.Parse(id),
                showroomid = Int32.Parse(showroomid),
                expensetypeid = Int32.Parse(expensetypeid),
                date = DateTime.Parse(date),
                amount = Decimal.Parse(amount),
                note = note,
                createby = createby,
                createdate = createdate
            };


            try
            {
                var employee = Mapper.Map<OtherExpenseDto, OtherExpense>(employeeDto);
                _context.OtherExpenses.Add(employee);
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
        public IHttpActionResult UpdateOtherExpense(int id)
        {
            //var id = HttpContext.Current.Request.Form["id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var expensetypeid = HttpContext.Current.Request.Form["expensetypeid"];
            var date = HttpContext.Current.Request.Form["date"];
            var amount = HttpContext.Current.Request.Form["amount"];
            var note = HttpContext.Current.Request.Form["note"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;

            var empInDb = _context.OtherExpenses.SingleOrDefault(c => c.id == id);
            var employeeDto = new OtherExpenseDto()
            {
                id = id,
                showroomid = Int32.Parse(showroomid),
                expensetypeid = Int32.Parse(expensetypeid),
                date = DateTime.Parse(date),
                amount = Decimal.Parse(amount),
                note = note,
                createby = createby,
                createdate = createdate
            };
            Mapper.Map(employeeDto, empInDb);
            _context.SaveChanges();
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
