using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using System.IO;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class StaffsController : ApiController
    {
        private ApplicationDbContext _context;

        public StaffsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Staff
        public IHttpActionResult GetStaff()
        {
            var getStaff = _context.Staffs.Include(c => c.position).ToList();
            return Ok(getStaff);
        }


        [HttpGet]
        //Get : api/RoomTypes{id}
        public IHttpActionResult GetStaffById(int id)
        {
            var getStaffById = _context.Staffs.Include(c => c.position).SingleOrDefault(c => c.id == id);

            if (getStaffById == null)
                return NotFound();

            return Ok(Mapper.Map<Staff, StaffDto>(getStaffById));
        }

            
        [HttpPost]
        public IHttpActionResult InsertStaff()
        {
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
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Staffs.SingleOrDefault(c => c.name == name && c.status == true);

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

            var staffDto = new StaffDto()
            {
                positionid = Int32.Parse(positionid),
                name = name,
                namekh = namekh,
                sex = sex,
                phone = phone,
                dob = DateTime.Parse(dob),
                address = address,
                email = email,
                identityno = identityno,
                photo = photoName,
                status = true,
                createby = createby,
                createdate = createdate
            };

            var staff = Mapper.Map<StaffDto, Staff>(staffDto);
            _context.Staffs.Add(staff);
            _context.SaveChanges();

            staffDto.id = staff.id;

            return Created(new Uri(Request.RequestUri + "/" + staffDto.id), staffDto);
            
        }


        [HttpPut]
        public IHttpActionResult UpdateEmployees(int id)
        {
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
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;
            var old_file = HttpContext.Current.Request.Form["file_old"];


            var empInDb = _context.Staffs.SingleOrDefault(c => c.id == id);

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
                var employeeDto = new StaffDto()
                {
                    id = id,
                    positionid = Int32.Parse(positionid),
                    name = name,
                    namekh = namekh,
                    sex = sex,
                    phone = phone,
                    dob = DateTime.Parse(dob),
                    address = address,
                    email = email,
                    identityno = identityno,
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

                var employeeDto = new StaffDto()
                {
                    id = id,
                    positionid = Int32.Parse(positionid),
                    name = name,
                    namekh = namekh,
                    sex = sex,
                    phone = phone,
                    dob = DateTime.Parse(dob),
                    address = address,
                    email = email,
                    identityno = identityno,
                    photo = old_file,
                    status = true,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(employeeDto, empInDb);
                _context.SaveChanges();

            }
            return Ok(new { });

        }


        [HttpDelete]
        //PUT : /api/Staffs/{id}
        public IHttpActionResult DeleteStaff(int id)
        {

            var empInDb = _context.Staffs.SingleOrDefault(c => c.id == id);
            if (empInDb == null)
                return BadRequest();
            _context.Staffs.Remove(empInDb);
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

