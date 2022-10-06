using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class ExpenseTypeController : ApiController
    {
        private ApplicationDbContext _context;
        public ExpenseTypeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET : /api/Departments  for get all record
        [HttpGet]
        public IHttpActionResult GetExpenseType()
        {
            var deparment = _context.ExpenseTypes.ToList().Select(Mapper.Map<ExpenseType, ExpenseTypeDto>);
            return Ok(deparment);
        }

        //GET : /api/Departments/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetExpenseType(int id)
        {
            var deparment = _context.ExpenseTypes.SingleOrDefault(c => c.id == id);
            if (deparment == null)
                return NotFound();

            return Ok(Mapper.Map<ExpenseType, ExpenseTypeDto>(deparment));
        }

        //POS : /api/Departments   for Insert record
        [HttpPost]
        public IHttpActionResult CreateExpenseType(ExpenseTypeDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.ExpenseTypes.SingleOrDefault(c => c.typename == categoryDto.typename);
            if (isExists != null)
                return BadRequest();

            var department = Mapper.Map<ExpenseTypeDto, ExpenseType>(categoryDto);
            //department.status = true;
            _context.ExpenseTypes.Add(department);
            _context.SaveChanges();
            categoryDto.id = department.id;

            return Created(new Uri(Request.RequestUri + "/" + categoryDto.id), categoryDto);


        }

        //PUT : /api/Departments/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateExpenseType(int id, ExpenseTypeDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.ExpenseTypes.SingleOrDefault(c => c.typename == categoryDto.typename && c.id != categoryDto.id);
            if (isExists != null)
                return BadRequest();
            var DepartmentInDb = _context.ExpenseTypes.SingleOrDefault(c => c.id == id);
            //DepartmentInDb.status = true;
            Mapper.Map(categoryDto, DepartmentInDb);
            _context.SaveChanges();
            return Ok(categoryDto);

        }

        //DELETE : /api/Departments/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteExpenseType(int id)
        {
            var DepartmentInDb = _context.ExpenseTypes.SingleOrDefault(c => c.id == id);
            if (DepartmentInDb == null)
                return BadRequest();

            var isExists = _context.OtherExpenses.Where(c => c.expensetypeid == id).Take(1);
            if (isExists.Count() > 0)
                return BadRequest();


            _context.ExpenseTypes.Remove(DepartmentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }

    }
}
