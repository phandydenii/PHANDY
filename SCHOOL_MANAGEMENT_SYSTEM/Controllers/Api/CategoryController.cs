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
    public class CategoryController : ApiController
    {
        private ApplicationDbContext _context;
        public CategoryController()
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
            var deparment = _context.Category.ToList().Select(Mapper.Map<Category, CategoryDto>);
            return Ok(deparment);
        }

        //GET : /api/Departments/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetCategory(int id)
        {
            var deparment = _context.Category.SingleOrDefault(c => c.id == id);
            if (deparment == null)
                return NotFound();

            return Ok(Mapper.Map<Category, CategoryDto>(deparment));
        }

        //POS : /api/Departments   for Insert record
        [HttpPost]
        public IHttpActionResult CreateCategory(CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Category.SingleOrDefault(c => c.categoryname == categoryDto.categoryname);
            if (isExists != null)
                return BadRequest();
            
            var department = Mapper.Map<CategoryDto, Category>(categoryDto);
            department.status = true;
            _context.Category.Add(department);
            _context.SaveChanges();
            categoryDto.id = department.id;

            return Created(new Uri(Request.RequestUri + "/" + categoryDto.id), categoryDto);


        }

        //PUT : /api/Departments/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateDepartment(int id, CategoryDto categoryDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Category.SingleOrDefault(c => c.categoryname == categoryDto.categoryname && c.id != categoryDto.id);
            if (isExists != null)
                return BadRequest();
            var DepartmentInDb = _context.Category.SingleOrDefault(c => c.id == id);
            DepartmentInDb.status = true;
            Mapper.Map(categoryDto, DepartmentInDb);
            _context.SaveChanges();
            return Ok(categoryDto);

        }

        //DELETE : /api/Departments/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteCategory(int id)
        {
            var DepartmentInDb = _context.Category.SingleOrDefault(c => c.id == id);
            if (DepartmentInDb == null)
                return BadRequest();

            var isExists = _context.Product.Where(c => c.categoryid == id).Take(1);
            if (isExists.Count() > 0)
                return BadRequest();


            _context.Category.Remove(DepartmentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }

    }
}
