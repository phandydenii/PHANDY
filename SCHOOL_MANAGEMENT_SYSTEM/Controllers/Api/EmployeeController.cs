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
    public class EmployeeController : ApiController
    {
        private ApplicationDbContext _context;
        public EmployeeController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Employees?departmentid={de..id}  for get all record
        [HttpGet]
        public IHttpActionResult GetEmployee(string departmentid)
        {

            if (departmentid == "all")
            {
                var employees = _context.Employee
                                            .Include(c => c.Showroom)
                                            .Include(c => c.Department).ToList().Select(Mapper.Map<Employee, EmployeeDto>)
                                            .Where(c=>c.status==true);
                return Ok(employees);
            }
            else
            {
                var employees = _context.Employee
                                            .Include(c => c.Showroom)
                                            .Include(c => c.Department).Select(Mapper.Map<Employee, EmployeeDto>)
                                            .Where(c => c.departmentid == int.Parse(departmentid) && c.status == true);
                return Ok(employees);
            }
            //var employees = _context.Employees.ToList().Select(Mapper.Map<Employee, EmployeeDto>);
            //return Ok(employees);


        }

        //GET : /api/Employees/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetEmployee(int id)
        {
            var employees = _context.Employee.Include(c => c.Showroom).Include(c => c.Department).Include(c => c.Position).SingleOrDefault(c => c.Id == id && c.status == true);
            //var employees = _context.Employee.SingleOrDefault(c => c.Id == id && c.status == true);
            if (employees == null)
                return NotFound();

            return Ok(Mapper.Map<Employee, EmployeeDto>(employees));
        }

        //POS : /api/Employees   for Insert record
        [HttpPost]
        public IHttpActionResult CreateEmployee()
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var showroomid = HttpContext.Current.Request.Form["showroomid"];
            var departmentid = HttpContext.Current.Request.Form["departmentid"];
            var positionid = HttpContext.Current.Request.Form["positionid"];
            var name = HttpContext.Current.Request.Form["name"];
            var namekh = HttpContext.Current.Request.Form["namekh"];
            var sex = HttpContext.Current.Request.Form["sex"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var dob = HttpContext.Current.Request.Form["dob"];
            var address = HttpContext.Current.Request.Form["address"];
            var email = HttpContext.Current.Request.Form["email"];
            var identityno = HttpContext.Current.Request.Form["identityno"];
            var photo = HttpContext.Current.Request.Files["photo"];
            var shippertype = HttpContext.Current.Request.Form["shippertype"];
            var vehiracle = HttpContext.Current.Request.Form["vehiracle"];
            var plateno = HttpContext.Current.Request.Form["plateno"];
            var phone_card = HttpContext.Current.Request.Form["phone_card"];
            var petroluem = HttpContext.Current.Request.Form["petroluem"];
            var deliveryin = HttpContext.Current.Request.Form["deliveryin"];
            var deliveryout = HttpContext.Current.Request.Form["deliveryout"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Employee.SingleOrDefault(c => c.name == name && c.status == true);

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

            var employeeDto = new EmployeeDto()
            {
                //Id = Int32.Parse(id),
                showroomid = Int32.Parse(showroomid),
                departmentid = Int32.Parse(departmentid),
                positionid = Int32.Parse(positionid),
                name = name,
                namekh = namekh,
                sex = sex,
                phone = phone,
                email = email,
                address = address,
                photo = photoName,
                dob = DateTime.Parse(dob),
                identityno = identityno,
                shippertype=shippertype,
                vehiracle=vehiracle,
                plateno=plateno,
                phone_card=decimal.Parse(phone_card),
                petroluem= decimal.Parse(petroluem),
                deliveryin= decimal.Parse(deliveryin),
                deliveryout= decimal.Parse(deliveryout),
                status = true,
                createby = createby,
                createdate = createdate
            };


            try
            {
                var employee = Mapper.Map<EmployeeDto, Employee>(employeeDto);
                _context.Employee.Add(employee);
                _context.SaveChanges();

                employeeDto.Id = employee.Id;

                return Created(new Uri(Request.RequestUri + "/" + employeeDto.Id), employeeDto);
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
            var departmentid = HttpContext.Current.Request.Form["departmentid"];
            var positionid = HttpContext.Current.Request.Form["positionid"];
            var name = HttpContext.Current.Request.Form["name"];
            var namekh = HttpContext.Current.Request.Form["namekh"];
            var sex = HttpContext.Current.Request.Form["sex"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var dob = HttpContext.Current.Request.Form["dob"];
            var address = HttpContext.Current.Request.Form["address"];
            var email = HttpContext.Current.Request.Form["email"];
            var identityno = HttpContext.Current.Request.Form["identityno"];
            var photo = HttpContext.Current.Request.Files["photo"];
            var shippertype = HttpContext.Current.Request.Form["shippertype"];
            var vehiracle = HttpContext.Current.Request.Form["vehiracle"];
            var plateno = HttpContext.Current.Request.Form["plateno"];
            var phone_card = HttpContext.Current.Request.Form["phone_card"];
            var petroluem = HttpContext.Current.Request.Form["petroluem"];
            var deliveryin = HttpContext.Current.Request.Form["deliveryin"];
            var deliveryout = HttpContext.Current.Request.Form["deliveryout"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Employee.SingleOrDefault(c => c.Id == id);

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
                var employeeDto = new EmployeeDto()
                {
                    Id = id,
                    showroomid = Int32.Parse(showroomid),
                    departmentid = Int32.Parse(departmentid),
                    positionid = Int32.Parse(positionid),
                    name = name,
                    namekh = namekh,
                    sex = sex,
                    phone = phone,
                    email = email,
                    address = address,
                    photo = photoName,
                    dob = DateTime.Parse(dob),
                    identityno = identityno,
                    shippertype = shippertype,
                    vehiracle = vehiracle,
                    plateno = plateno,
                    phone_card = decimal.Parse(phone_card),
                    petroluem = decimal.Parse(petroluem),
                    deliveryin = decimal.Parse(deliveryin),
                    deliveryout = decimal.Parse(deliveryout),
                    status = true,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();


            }
            else
            {

                var employeeDto = new EmployeeDto()
                {
                    Id = id,
                    showroomid = Int32.Parse(showroomid),
                    departmentid = Int32.Parse(departmentid),
                    positionid = Int32.Parse(positionid),
                    name = name,
                    namekh = namekh,
                    sex = sex,
                    phone = phone,
                    email = email,
                    address = address,
                    photo = photoName,
                    dob = DateTime.Parse(dob),
                    identityno = identityno,
                    shippertype = shippertype,
                    vehiracle = vehiracle,
                    plateno = plateno,
                    phone_card = decimal.Parse(phone_card),
                    petroluem = decimal.Parse(petroluem),
                    deliveryin = decimal.Parse(deliveryin),
                    deliveryout = decimal.Parse(deliveryout),
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
        public IHttpActionResult DeleteEmployee(int id)
        {
            var empInDb = _context.Employee.SingleOrDefault(c => c.Id == id);
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
