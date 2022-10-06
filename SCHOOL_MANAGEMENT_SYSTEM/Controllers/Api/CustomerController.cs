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
    public class CustomerController : ApiController
    {
        private ApplicationDbContext _context;
        public CustomerController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Employees?departmentid={de..id}  for get all record
        [HttpGet]
        public IHttpActionResult GetCustomer(string showroomId)
        {

            if (showroomId == "all")
            {
                var employees = _context.Customer
                                            .Include(c => c.Showroom).ToList().Select(Mapper.Map<Customer, CustomerDto>)
                                            .Where(c => c.status == true);
                return Ok(employees);
            }
            else
            {
                var employees = _context.Customer
                                            .Include(c => c.Showroom).Select(Mapper.Map<Customer, CustomerDto>)
                                            .Where(c => c.showroomid == int.Parse(showroomId) && c.status == true);
                return Ok(employees);
            }
            //var employees = _context.Employees.ToList().Select(Mapper.Map<Employee, EmployeeDto>);
            //return Ok(employees);


        }

        //GET : /api/Employees/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetCustomer(int id)
        {
            var employees = _context.Customer.Include(c => c.Showroom).SingleOrDefault(c => c.Id == id && c.status == true);
            //var employees = _context.Employee.SingleOrDefault(c => c.Id == id && c.status == true);
            if (employees == null)
                return NotFound();

            return Ok(Mapper.Map<Customer, CustomerDto>(employees));
        }

        //POS : /api/Employees   for Insert record
        [HttpPost]
        public IHttpActionResult CreateCustomer()
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var name = HttpContext.Current.Request.Form["name"];
            var sex = HttpContext.Current.Request.Form["sex"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var address = HttpContext.Current.Request.Form["address"];
            var identityno = HttpContext.Current.Request.Form["identityno"];
            var photo = HttpContext.Current.Request.Files["photo"];
            var customertype = HttpContext.Current.Request.Form["customertype"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Customer.SingleOrDefault(c => c.name == name && c.status == true);

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

            var customerDto = new CustomerDto()
            {
                //Id = Int32.Parse(id),
                showroomid = Int32.Parse(showroomid),
                name = name,
                sex = sex,
                phone = phone,
                address = address,
                photo = photoName,
                identityno = identityno,
                customertype = customertype,
                status = true,
                createby = createby,
                createdate = createdate
            };


            try
            {
                var employee = Mapper.Map<CustomerDto, Customer>(customerDto);
                _context.Customer.Add(employee);
                _context.SaveChanges();

                customerDto.Id = employee.Id;

                return Created(new Uri(Request.RequestUri + "/" + customerDto.Id), customerDto);
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
        public IHttpActionResult UpdateEmployee(int id)
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var name = HttpContext.Current.Request.Form["name"];
            var sex = HttpContext.Current.Request.Form["sex"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var address = HttpContext.Current.Request.Form["address"];
            var identityno = HttpContext.Current.Request.Form["identityno"];
            var photo = HttpContext.Current.Request.Files["photo"];
            var customertype = HttpContext.Current.Request.Form["customertype"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Customer.SingleOrDefault(c => c.Id == id);

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
                var customerDto = new CustomerDto()
                {
                    Id = id,
                    showroomid = Int32.Parse(showroomid),
                    name = name,
                    sex = sex,
                    phone = phone,
                    address = address,
                    photo = photoName,
                    identityno = identityno,
                    customertype = customertype,
                    status = true,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(customerDto, empInDb);
                _context.SaveChanges();


            }
            else
            {

                var customerDto = new CustomerDto()
                {
                    Id = id,
                    showroomid = Int32.Parse(showroomid),
                    name = name,
                    sex = sex,
                    phone = phone,
                    address = address,
                    photo = photoName,
                    identityno = identityno,
                    customertype = customertype,
                    status = true,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(customerDto, empInDb);
                _context.SaveChanges();

            }
            return Ok(new { });

        }
        //DELETE : /api/Employees/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteEmployee(int id)
        {
            var empInDb = _context.Customer.SingleOrDefault(c => c.Id == id);
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
