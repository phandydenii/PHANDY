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
    public class EmployeesController : ApiController
    {
        private ApplicationDbContext _context;
        public EmployeesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Employees?departmentid={de..id}  for get all record
        [HttpGet]
        public IHttpActionResult GetEmployees(string departmentid)
        {

            if (departmentid == "all")
            {
                var employees = _context.Employees
                                            .Include(c => c.Branch)
                                            .Include(c => c.Department).ToList().Select(Mapper.Map<Employees, EmployeesDto>);
                return Ok(employees);
            }
            else
            {
                var employees = _context.Employees
                                            .Include(c => c.Branch)
                                            .Include(c => c.Department).Select(Mapper.Map<Employees, EmployeesDto>)
                                            .Where(c => c.DepartmentId == int.Parse(departmentid)); //(c => c.is_active == true);
                return Ok(employees);
            }
            //var employees = _context.Employees.ToList().Select(Mapper.Map<Employee, EmployeeDto>);
            //return Ok(employees);


        }

        //GET : /api/Employees/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetEmployees(int id)
        {
            var employees = _context.Employees.Include(c => c.Branch).Include(c => c.Department).SingleOrDefault(c => c.Id == id && c.is_active==true);
            if (employees == null)
                return NotFound();

            return Ok(Mapper.Map<Employees, EmployeesDto>(employees));
        }

        //POS : /api/Employees   for Insert record
        [HttpPost]
        public IHttpActionResult CreateEmployees()
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var branchid = HttpContext.Current.Request.Form["branchId"];
            var departmentid = HttpContext.Current.Request.Form["departmentId"];
            var marital_staus = HttpContext.Current.Request.Form["maritalstatus"];
            var name = HttpContext.Current.Request.Form["employeename"];
            var namekh = HttpContext.Current.Request.Form["employeenamekh"];
            var gender = HttpContext.Current.Request.Form["gender"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var email = HttpContext.Current.Request.Form["email"];
            var emp_address = HttpContext.Current.Request.Form["address"];
            var img = HttpContext.Current.Request.Files["employeeImg"];
            var dob = HttpContext.Current.Request.Form["dob"];
            var pob = HttpContext.Current.Request.Form["pob"];
            var is_active = HttpContext.Current.Request.Form["is_active"];
            var create_by = User.Identity.GetUserName();
            var create_date = DateTime.Today;
            

            var empInDb = _context.Employees.SingleOrDefault(c => c.name == name && c.is_active==true);

            if (empInDb != null)
                return BadRequest();

            string photoName = "";
            if (img != null) {
                photoName = Path.Combine(Path.GetDirectoryName(img.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(img.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(img.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                img.SaveAs(fileSavePath);

            }

            var employeeDto = new EmployeesDto() {
                //Id = Int32.Parse(id),
                BranchId =Int32.Parse(branchid),
                DepartmentId=Int32.Parse(departmentid),
                marital_Status=marital_staus,
                name=name,
                name_kh=namekh,
                gender=gender,
                phone=phone,
                email=email,
                emp_address=emp_address,
                img=photoName,
                dob=DateTime.Parse(dob),
                pob=pob,
                is_active=true,
                create_by=create_by,
                create_date=create_date
            };


            try
            {
                var employee = Mapper.Map<EmployeesDto, Employees>(employeeDto);
                _context.Employees.Add(employee);
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
        public IHttpActionResult UpdateEmployees(int id)
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var branchid = HttpContext.Current.Request.Form["branchId"];
            var departmentid = HttpContext.Current.Request.Form["departmentId"];
            var marital_staus = HttpContext.Current.Request.Form["marintalstatus"];
            var name = HttpContext.Current.Request.Form["employeename"];
            var namekh = HttpContext.Current.Request.Form["employeenamekh"];
            var gender = HttpContext.Current.Request.Form["gender"];
            var phone = HttpContext.Current.Request.Form["phone"];
            var email = HttpContext.Current.Request.Form["email"];
            var emp_address = HttpContext.Current.Request.Form["address"];
            var img = HttpContext.Current.Request.Files["employeeImg"];
            var dob = HttpContext.Current.Request.Form["dob"];
            var pob = HttpContext.Current.Request.Form["pob"];
            var is_active = HttpContext.Current.Request.Form["is_active"];
            var create_by = User.Identity.GetUserName();
            var create_date = DateTime.Today;
            var old_file = HttpContext.Current.Request.Form["file_old"];


            var empInDb = _context.Employees.SingleOrDefault(c => c.Id==id);

            string photoName = "";
            if (img != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(img.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(img.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(img.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                img.SaveAs(fileSavePath);


                //Delete OldPhoto
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.img);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
                var employeeDto = new EmployeesDto()
                {
                    Id = id,
                    BranchId = Int32.Parse(branchid),
                    DepartmentId = Int32.Parse(departmentid),
                    marital_Status = marital_staus,
                    name = name,
                    name_kh = namekh,
                    gender = gender,
                    phone = phone,
                    email = email,
                    emp_address = emp_address,
                    img = photoName,
                    dob = DateTime.Parse(dob),
                    pob = pob,
                    is_active = true,
                    create_by = create_by,
                    create_date = create_date
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();


            }
            else {
                
                var employeeDto = new EmployeesDto()
                {
                    Id = id,
                    BranchId = Int32.Parse(branchid),
                    DepartmentId = Int32.Parse(departmentid),
                    marital_Status = marital_staus,
                    name = name,
                    name_kh = namekh,
                    gender = gender,
                    phone = phone,
                    email = email,
                    emp_address = emp_address,
                    img = old_file,
                    dob = DateTime.Parse(dob),
                    pob = pob,
                    is_active = true,
                    create_by = create_by,
                    create_date = create_date
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
            var empInDb = _context.Employees.SingleOrDefault(c => c.Id == id);
            if (empInDb == null)
                return BadRequest();

            empInDb.is_active = false;
            _context.SaveChanges();

            var photoPart = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.img);
            if (File.Exists(photoPart)) {
                File.Delete(photoPart);
            }
            return Ok(new { });


        }
    }
}
