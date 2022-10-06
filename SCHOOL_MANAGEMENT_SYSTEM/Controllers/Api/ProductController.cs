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
    public class ProductController : ApiController
    {
        private ApplicationDbContext _context;
        public ProductController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Employees?departmentid={de..id}  for get all record
        [HttpGet]
        public IHttpActionResult GetProduct(string showroomId)
        {

            if (showroomId == "all")
            {
                var employees = _context.Product
                                            .Include(c => c.Showrooms).ToList().Select(Mapper.Map<Product, ProductDto>)
                                            .Where(c => c.status == true);
                return Ok(employees);
            }
            else
            {
                var employees = _context.Product
                                            .Include(c => c.Showrooms).Select(Mapper.Map<Product, ProductDto>)
                                            .Where(c => c.showroomid == int.Parse(showroomId) && c.status == true);
                return Ok(employees);
            }
            //var employees = _context.Employees.ToList().Select(Mapper.Map<Employee, EmployeeDto>);
            //return Ok(employees);


        }

        //GET : /api/Employees/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetProduct(int id)
        {
            var employees = _context.Product.Include(c => c.Showrooms).SingleOrDefault(c => c.id == id && c.status == true);
            //var employees = _context.Employee.SingleOrDefault(c => c.Id == id && c.status == true);
            if (employees == null)
                return NotFound();

            return Ok(Mapper.Map<Product, ProductDto>(employees));
        }

        //POS : /api/Employees   for Insert record
        [HttpPost]
        public IHttpActionResult CreateProduct()
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var categoryid = HttpContext.Current.Request.Form["categoryid"];
            var productname = HttpContext.Current.Request.Form["productname"];
            var qtyonhand = HttpContext.Current.Request.Form["qtyonhand"];
            var photo = HttpContext.Current.Request.Files["photo"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Product.SingleOrDefault(c => c.productname == productname && c.status == true);

            if (empInDb != null)
                return BadRequest();

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

            var employeeDto = new ProductDto()
            {
                //Id = Int32.Parse(id),
                showroomid = Int32.Parse(showroomid),
                categoryid= Int32.Parse(categoryid),
                productname = productname,
                qtyonhand = Decimal.Parse(qtyonhand),
                photo = photoName,
                status = true,
                createby = createby,
                createdate = createdate
            };


            try
            {
                var employee = Mapper.Map<ProductDto, Product>(employeeDto);
                _context.Product.Add(employee);
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
        public IHttpActionResult UpdateProduct(int id)
        {
            //var id = HttpContext.Current.Request.Form["id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var categoryid = HttpContext.Current.Request.Form["categoryid"];
            var productname = HttpContext.Current.Request.Form["productname"];
            var qtyonhand = HttpContext.Current.Request.Form["qtyonhand"];
            var photo = HttpContext.Current.Request.Files["photo"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Product.SingleOrDefault(c => c.id == id);

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
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.photo);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
                var employeeDto = new ProductDto()
                {
                    id = id,
                    showroomid = Int32.Parse(showroomid),
                    categoryid = Int32.Parse(categoryid),
                    productname = productname,
                    qtyonhand = Decimal.Parse(qtyonhand),
                    photo = photoName,
                    status = true,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();


            }
            else
            {

                var employeeDto = new ProductDto()
                {
                    id = id,
                    showroomid = Int32.Parse(showroomid),
                    categoryid = Int32.Parse(categoryid),
                    productname = productname,
                    qtyonhand = Decimal.Parse(qtyonhand),
                    photo = photoName,
                    status = true,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();

            }
            return Ok(new { });

        }
        //DELETE : /api/Employees/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteProduct(int id)
        {
            var empInDb = _context.Product.SingleOrDefault(c => c.id == id);
            if (empInDb == null)
                return BadRequest();

            empInDb.status = false;
            _context.SaveChanges();

            var photoPart = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.photo);
            if (File.Exists(photoPart))
            {
                File.Delete(photoPart);
            }
            return Ok(new { });


        }
    }
}
