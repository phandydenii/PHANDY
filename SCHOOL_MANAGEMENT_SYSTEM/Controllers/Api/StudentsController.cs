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
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class StudentsController : ApiController
    {
        private ApplicationDbContext _context;
        public StudentsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }


        //GET : /api/Students/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetStudents(string branchid)
        {
           
            if (branchid == "all")
            {
                var employees = _context.Students
                                            .Include(s => s.Shiftes)
                                            .Include(c => c.Grade).Select(Mapper.Map<student, studentDto>);
                return Ok(employees);
            }
            else
            {
                var employees = _context.Students
                                            .Include(s=>s.Shiftes)
                                            .Include(c => c.Branch)
                                            .Include(c => c.Grade).Select(Mapper.Map<student, studentDto>)
                                            .Where(c => c.studentbranchid == int.Parse(branchid)); //(c => c.is_active == true);
                return Ok(employees);
            }
        }

        //GET : /api/Students/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetStudents(int id)
        {
            var students = _context.Students.Include(c => c.Shiftes).Include(c => c.Grade).SingleOrDefault(c => c.id == id); //&& c.studentstatus == 'ACTIVE');
            if (students == null)
                return NotFound();

            return Ok(Mapper.Map<student, studentDto>(students));
        }

        [HttpGet]
        public IHttpActionResult GetMaxID(String a, String b)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(studentid) IS NULL THEN (1)ELSE MAX(studentid)+1 END AS ID,'SH' + RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(studentid) IS NULL THEN (1)ELSE MAX(studentid)+1 END )) , 6) AS StuID FROM Students", conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();
            string InvNoFormat = ds.Rows[0][1].ToString();
            return Ok(InvNo + "," + InvNoFormat);

        }

        //POS : /api/Students   for Insert record
        [HttpPost]
        public IHttpActionResult CreateStudents()
        {
            //var id = HttpContext.Current.Request.Form["Id"];
            var studentid = HttpContext.Current.Request.Form["studentid"];
            var studentbranchid = HttpContext.Current.Request.Form["studentbranchid"];
            var surname = HttpContext.Current.Request.Form["surname"];
            var firstname = HttpContext.Current.Request.Form["firstname"];
            var fullname = HttpContext.Current.Request.Form["fullname"];
            var fullnamekh = HttpContext.Current.Request.Form["fullnamekh"];
            var studentgender = HttpContext.Current.Request.Form["studentgender"];
            var studentdob = HttpContext.Current.Request.Form["studentdob"];
            var studentpob = HttpContext.Current.Request.Form["studentpob"];
            var nationality = HttpContext.Current.Request.Form["nationality"];
            var nativelanguage = HttpContext.Current.Request.Form["nativelanguage"];
            var otherspoken = HttpContext.Current.Request.Form["otherspoken"];
            var schoolyear = HttpContext.Current.Request.Form["schoolyear"];
            var studentphoto = HttpContext.Current.Request.Files["studentphoto"];
            var studentshiftid = HttpContext.Current.Request.Form["studentshiftid"];
            var studentgradeid = HttpContext.Current.Request.Form["studentgradeid"];
            var studentstatus = HttpContext.Current.Request.Form["studentstatus"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;


            var empInDb = _context.Students.SingleOrDefault(c => c.fullname == fullname && c.studentstatus == "ACTIVE");

            if (empInDb != null)
                return BadRequest();

            string photoName = "";
            if (studentphoto != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(studentphoto.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(studentphoto.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(studentphoto.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                studentphoto.SaveAs(fileSavePath);

            }

            var StudentDto = new studentDto()
            {
                //Id = Int32.Parse(id),
                studentid = Int32.Parse(studentid),
                studentbranchid = Int32.Parse(studentbranchid),
                surname = surname,
                firstname = firstname,
                fullname = fullname,
                fullnamekh = fullnamekh,
                studentgender = studentgender,
                studentdob = DateTime.Parse(studentdob),
                studentpob = studentpob,
                nationality=nationality,
                nativelanguage=nativelanguage,
                otherspoken=otherspoken,
                schoolyear=schoolyear,
                studentphoto = photoName,
                studentshiftid=Int32.Parse(studentshiftid),
                studentgradeid=Int32.Parse(studentgradeid),
                studentstatus=studentstatus,
                createby = createby,
                createdate = createdate
            };


            try
            {
                var student = Mapper.Map<studentDto, student>(StudentDto);
                _context.Students.Add(student);
                _context.SaveChanges();

                StudentDto.id = student.id;

                return Created(new Uri(Request.RequestUri + "/" + StudentDto.id), StudentDto);
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

        //PUT : /api/Students/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateStudents(int id)
        {
            //var sid = HttpContext.Current.Request.Form["id"];
            var studentid = HttpContext.Current.Request.Form["studentid"];
            var studentbranchid = HttpContext.Current.Request.Form["studentbranchid"];
            var surname = HttpContext.Current.Request.Form["surname"];
            var firstname = HttpContext.Current.Request.Form["firstname"];
            var fullname = HttpContext.Current.Request.Form["fullname"];
            var fullnamekh = HttpContext.Current.Request.Form["fullnamekh"];
            var studentgender = HttpContext.Current.Request.Form["studentgender"];
            var studentdob = HttpContext.Current.Request.Form["studentdob"];
            var studentpob = HttpContext.Current.Request.Form["studentpob"];
            var nationality = HttpContext.Current.Request.Form["nationality"];
            var nativelanguage = HttpContext.Current.Request.Form["nativelanguage"];
            var otherspoken = HttpContext.Current.Request.Form["otherspoken"];
            var schoolyear = HttpContext.Current.Request.Form["schoolyear"];
            var studentphoto = HttpContext.Current.Request.Files["studentphoto"];
            var studentshiftid = HttpContext.Current.Request.Form["studentshiftid"];
            var studentgradeid = HttpContext.Current.Request.Form["studentgradeid"];
            var studentstatus = HttpContext.Current.Request.Form["studentstatus"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;
            var old_file = HttpContext.Current.Request.Form["file_old"];


            var empInDb = _context.Students.SingleOrDefault(c => c.id == id);

            string photoName = "";
            if (studentphoto != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(studentphoto.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(studentphoto.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(studentphoto.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                studentphoto.SaveAs(fileSavePath);


                //Delete OldPhoto
                var oldPhotoPath= Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.studentphoto);
               
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
                var studentDto = new studentDto()
                {
                    id=id,
                    studentid = Int32.Parse(studentid),
                    studentbranchid = Int32.Parse(studentbranchid),
                    surname = surname,
                    firstname = firstname,
                    fullname = fullname,
                    fullnamekh = fullnamekh,
                    studentgender = studentgender,
                    studentdob = DateTime.Parse(studentdob),
                    studentpob = studentpob,
                    nationality = nationality,
                    nativelanguage = nativelanguage,
                    otherspoken = otherspoken,
                    schoolyear = schoolyear,
                    studentphoto = photoName,
                    studentshiftid = Int32.Parse(studentshiftid),
                    studentgradeid = Int32.Parse(studentgradeid),
                    studentstatus = studentstatus,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(studentDto, empInDb);
                _context.SaveChanges();


            }
            else
            {

                var studentdto = new studentDto()
                {
                    id = id,
                    studentid = Int32.Parse(studentid),
                    studentbranchid = Int32.Parse(studentbranchid),
                    surname = surname,
                    firstname = firstname,
                    fullname = fullname,
                    fullnamekh = fullnamekh,
                    studentgender = studentgender,
                    studentdob = DateTime.Parse(studentdob),
                    studentpob = studentpob,
                    nationality = nationality,
                    nativelanguage = nativelanguage,
                    otherspoken = otherspoken,
                    schoolyear = schoolyear,
                    studentphoto = old_file,
                    studentshiftid = Int32.Parse(studentshiftid),
                    studentgradeid = Int32.Parse(studentgradeid),
                    studentstatus = studentstatus,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(studentdto, empInDb);
                _context.SaveChanges();

            }
            return Ok(new { });

        }
        //DELETE : /api/Students/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteStudent(int id)
        {
            var empInDb = _context.Employees.SingleOrDefault(c => c.Id == id);
            if (empInDb == null)
                return BadRequest();

            empInDb.is_active = false;
            _context.SaveChanges();

            var photoPart = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.img);
            if (File.Exists(photoPart))
            {
                File.Delete(photoPart);
            }
            return Ok(new { });


        }
    }
}
