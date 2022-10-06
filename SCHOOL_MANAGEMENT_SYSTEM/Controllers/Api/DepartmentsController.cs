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
    public class DepartmentsController : ApiController
    {
        private ApplicationDbContext _context;
        public DepartmentsController(){
            _context =new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET : /api/Departments  for get all record
        [HttpGet]
        public IHttpActionResult GetDepartment()
        {
            var deparment = _context.Departments.ToList().Select(Mapper.Map<Department, DepartmentDto>);
            return Ok(deparment);
        }

        //GET : /api/Departments/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetDepartment(int id)
        {
            var deparment = _context.Departments.SingleOrDefault(c => c.Id == id);
            if (deparment == null)
                return NotFound();

            return Ok(Mapper.Map<Department, DepartmentDto>(deparment));
        }

        //POS : /api/Departments   for Insert record
        [HttpPost]
        public IHttpActionResult CreateDepartments(DepartmentDto DepartmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Departments.SingleOrDefault(c => c.Name == DepartmentDto.Name);
            if (isExists != null)
                return BadRequest();

            DepartmentDto.create_date = DateTime.Today;
            DepartmentDto.create_by = User.Identity.GetUserName();

            var department = Mapper.Map<DepartmentDto, Department>(DepartmentDto);
            _context.Departments.Add(department);
            _context.SaveChanges();
            DepartmentDto.Id = department.Id;

            return Created(new Uri(Request.RequestUri + "/" + DepartmentDto.Id), DepartmentDto);


        }

        //PUT : /api/Departments/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateDepartment(int id, DepartmentDto DepartmentDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Departments.SingleOrDefault(c => c.Name == DepartmentDto.Name && c.Id != DepartmentDto.Id);
            if (isExists != null)
                return BadRequest();
            var DepartmentInDb = _context.Departments.SingleOrDefault(c => c.Id == id);
            DepartmentDto.create_date = DateTime.Today;
            DepartmentDto.create_by = User.Identity.GetUserName();
            Mapper.Map(DepartmentDto, DepartmentInDb);
            _context.SaveChanges();
            return Ok(DepartmentDto);

        }

        //DELETE : /api/Departments/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteDepartment(int id)
        {
            var DepartmentInDb = _context.Departments.SingleOrDefault(c => c.Id == id);
            if (DepartmentInDb == null)
                return BadRequest();

            var isExists = _context.Employees.Where(c => c.DepartmentId == id ).Take(1);
            if (isExists.Count() > 0 )
                return BadRequest();


            _context.Departments.Remove(DepartmentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }

    }
}
