using AutoMapper;
using Microsoft.AspNet.Identity;
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
    public class Invoice_MoveController : ApiController
    {
        private ApplicationDbContext _context;
        public Invoice_MoveController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET : /api/Departments  for get all record
        [HttpGet]
        public IHttpActionResult GetCategory()
        {
            var deparment = _context.InvoiceMoves.ToList().Select(Mapper.Map<invoice_move, invoice_moveDto>);
            return Ok(deparment);
        }

        //GET : /api/Departments/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            var deparment = _context.InvoiceMoves.SingleOrDefault(c => c.id == id);
            if (deparment == null)
                return NotFound();

            return Ok(Mapper.Map<invoice_move, invoice_moveDto>(deparment));
        }

        //POS : /api/Departments   for Insert record
        [HttpPost]
        public IHttpActionResult CreateCategory(invoice_moveDto categoryDto)
        {
           

            var isExists = _context.InvoiceMoves.SingleOrDefault(c => c.invoiceid == categoryDto.invoiceid);
            if (isExists != null)
                return BadRequest();

            var department = Mapper.Map<invoice_moveDto, invoice_move>(categoryDto);
            
            department.moveby = User.Identity.GetUserName();
            department.date = DateTime.Today;
            department.note = "Move";
            _context.InvoiceMoves.Add(department);
            _context.SaveChanges();
            categoryDto.id = department.id;

            return Created(new Uri(Request.RequestUri + "/" + categoryDto.id), categoryDto);


        }

        //PUT : /api/Departments/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateDepartment(int id, invoice_moveDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.InvoiceMoves.SingleOrDefault(c => c.invoiceid == categoryDto.invoiceid && c.id != categoryDto.id);
            if (isExists != null)
                return BadRequest();
            var DepartmentInDb = _context.InvoiceMoves.SingleOrDefault(c => c.id == id);
            
            Mapper.Map(categoryDto, DepartmentInDb);
            _context.SaveChanges();
            return Ok(categoryDto);

        }

        //DELETE : /api/Departments/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var DepartmentInDb = _context.InvoiceMoves.SingleOrDefault(c => c.id == id);
            if (DepartmentInDb == null)
                return BadRequest();

            _context.InvoiceMoves.Remove(DepartmentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }

    }
}
