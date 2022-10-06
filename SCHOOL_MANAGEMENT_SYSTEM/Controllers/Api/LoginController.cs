using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
        [Authorize]
        public class LoginController : ApiController
        {
            private ApplicationDbContext _context;
            public LoginController()
            {
                _context = new ApplicationDbContext();

            }
            protected override void Dispose(bool disposing)
            {
                _context.Dispose();
            }
            //POS : /api/Login   for Insert record
            [HttpPost]
            public IHttpActionResult Login()
            {
                var phone = HttpContext.Current.Request.Form["phone"];
                var email = HttpContext.Current.Request.Form["email"];
                var password = HttpContext.Current.Request.Form["password"];
                
                var empInDb = _context.Employees.SingleOrDefault(c => c.phone == phone || c.email==email && c.password==password && c.is_active == true);

                if (empInDb != null){
                    return BadRequest();
                }else {
                    return Ok();
                }
            
            }
        //public IHttpActionResult CreateEmployees()
        //{
        //    //var id = HttpContext.Current.Request.Form["Id"];
        //    var branchid = 1;//HttpContext.Current.Request.Form["branchId"];
        //    var departmentid = 1;//HttpContext.Current.Request.Form["departmentId"];
        //    var marital_staus = "Single";//HttpContext.Current.Request.Form["maritalstatus"];
        //    var name = HttpContext.Current.Request.Form["employeename"];
        //    var namekh = HttpContext.Current.Request.Form["employeenamekh"];
        //    var gender = HttpContext.Current.Request.Form["gender"];
        //    var phone = HttpContext.Current.Request.Form["phone"];
        //    var email = HttpContext.Current.Request.Form["email"];
        //    var emp_address = HttpContext.Current.Request.Form["address"];
        //    var img = HttpContext.Current.Request.Files["employeeImg"];
        //    var dob = HttpContext.Current.Request.Form["dob"];
        //    var pob = HttpContext.Current.Request.Form["pob"];
        //    var is_active = HttpContext.Current.Request.Form["is_active"];
        //    var create_by = User.Identity.GetUserName();
        //    var create_date = DateTime.Today;


        //    var empInDb = _context.Employees.SingleOrDefault(c => c.name == name && c.is_active == true);

        //    if (empInDb != null)
        //        return BadRequest();

        //    string photoName = "";
        //    if (img != null)
        //    {
        //        photoName = Path.Combine(Path.GetDirectoryName(img.FileName)
        //            , string.Concat(Path.GetFileNameWithoutExtension(img.FileName)
        //            , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
        //            , Path.GetExtension(img.FileName)
        //            ));
        //        var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
        //        img.SaveAs(fileSavePath);

        //    }

        //    var employeeDto = new EmployeeDto()
        //    {
        //        //Id = Int32.Parse(id),
        //        BranchId = 1,//Int32.Parse(branchid),
        //        DepartmentId = 1,//Int32.Parse(departmentid),
        //        marital_Status = marital_staus,
        //        name = name,
        //        name_kh = namekh,
        //        gender = gender,
        //        phone = phone,
        //        email = email,
        //        emp_address = emp_address,
        //        img = photoName,
        //        dob = DateTime.Parse(dob),
        //        pob = pob,
        //        is_active = true,
        //        create_by = create_by,
        //        create_date = create_date
        //    };


        //    try
        //    {
        //        var employee = Mapper.Map<EmployeeDto, Employee>(employeeDto);
        //        _context.Employees.Add(employee);
        //        _context.SaveChanges();

        //        employeeDto.Id = employee.Id;

        //        return Created(new Uri(Request.RequestUri + "/" + employeeDto.Id), employeeDto);
        //    }
        //    catch (DbEntityValidationException e)
        //    {
        //        foreach (var eve in e.EntityValidationErrors)
        //        {
        //            Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
        //                eve.Entry.Entity.GetType().Name, eve.Entry.State);
        //            foreach (var ve in eve.ValidationErrors)
        //            {
        //                Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
        //                    ve.PropertyName, ve.ErrorMessage);
        //            }
        //        }
        //        throw;
        //    }
        //}
    }
    }

