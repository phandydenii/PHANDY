using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.Entity.Validation;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{

    [Authorize]
    public class RegisterstudentsController : ApiController
    {
        private ApplicationDbContext _context;
        public RegisterstudentsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        public IHttpActionResult GetMaxID(String a, String b)
        {
            //For Get Max PaymentNo +1
            DataTable ds = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("SELECT CASE WHEN MAX(registerid) IS NULL THEN (1)ELSE MAX(registerid)+1 END AS ID,'R' + RIGHT('000000' + CONVERT(NVARCHAR, (SELECT CASE WHEN MAX(registerid) IS NULL THEN (1)ELSE MAX(registerid)+1 END )) , 6) AS InvoiceID FROM Registerstudents", conx);
            adp.Fill(ds);
            string InvNo = ds.Rows[0][0].ToString();
            string InvNoFormat = ds.Rows[0][1].ToString();
            return Ok(InvNo + "," + InvNoFormat);

        }

        [HttpGet]
        [ResponseType(typeof(registerstudent))]
        [AllowAnonymous]
        public IHttpActionResult GetRegister()
        {

            var brans = from r in _context.Registerstudents
                        join s in _context.Students on r.studentid equals s.id
                        join sf in _context.Shiftes on r.shiftid equals sf.shiftid
                        join l in _context.studylanguages on r.languageid equals l.studylanguageid
                        join p in _context.studyperiods on r.periodid equals p.studyperiodid
                        join g in _context.Grades on r.gradeid equals g.gradeid
                        select new RegisterStudentV
                        {
                            id = r.id,
                            registerid = r.registerid,
                            date = r.date,
                            studentid = r.studentid,
                            fullname = s.fullnamekh,
                            shiftid = r.shiftid,
                            shiftname = sf.shiftname,
                            languageid = r.languageid,
                            language = l.language,
                            periodid = r.periodid,
                            period = p.period,
                            gradeid = r.gradeid,
                            gradename = g.gradename,
                            registerfile=r.registerfile,
                            status = r.status
                        };

            return Ok(brans);
        }



        //GET : /api/Parents/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetRegister(int id)
        {
            var parents = _context.Registerstudents.SingleOrDefault(c => c.id == id);
            if (parents == null)
                return NotFound();

            return Ok(Mapper.Map<registerstudent, registerstudentDto>(parents));

        }


        //POS : /api/Parents   for Insert record
        [HttpPost]
        public IHttpActionResult CreateRegister()
        {
            var registerid = HttpContext.Current.Request.Form["registerid"];
            var date = HttpContext.Current.Request.Form["date"];
            var userid = HttpContext.Current.Request.Form["userid"];
            var studentid = HttpContext.Current.Request.Form["studentid"];
            var shiftid = HttpContext.Current.Request.Form["shiftid"];
            var languageid = HttpContext.Current.Request.Form["languageid"];
            var periodid = HttpContext.Current.Request.Form["periodid"];
            var gradeid = HttpContext.Current.Request.Form["gradeid"];
            var registerfile = HttpContext.Current.Request.Files["registerfile"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;

            string photoName = "";
            if (registerfile != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(registerfile.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(registerfile.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(registerfile.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                registerfile.SaveAs(fileSavePath);

            }

            var registerDto = new registerstudentDto()
            {
                //Id = Int32.Parse(id),
                registerid = Int32.Parse(registerid),
                date = DateTime.Parse(date),
                studentid = Int32.Parse(studentid),
                userid = User.Identity.GetUserId(),
                shiftid = Int32.Parse(shiftid),
                languageid = Int32.Parse(languageid),
                periodid = Int32.Parse(periodid),
                gradeid = Int32.Parse(gradeid),
                registerfile = photoName,
                status = status,
                createby = createby,
                createdate = createdate
            };
            try
            {
                var student = Mapper.Map<registerstudentDto, registerstudent>(registerDto);
                _context.Registerstudents.Add(student);
                _context.SaveChanges();
                registerDto.id = student.id;

                return Created(new Uri(Request.RequestUri + "/" + registerDto.id), registerDto);
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


        //PUT : /api/Parents/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateParentstudent(int id)
        {
            var registerid = HttpContext.Current.Request.Form["registerid"];
            var date = HttpContext.Current.Request.Form["date"];
            var userid = HttpContext.Current.Request.Form["userid"];
            var studentid = HttpContext.Current.Request.Form["studentid"];
            var shiftid = HttpContext.Current.Request.Form["shiftid"];
            var languageid = HttpContext.Current.Request.Form["languageid"];
            var periodid = HttpContext.Current.Request.Form["periodid"];
            var gradeid = HttpContext.Current.Request.Form["gradeid"];
            var registerfile = HttpContext.Current.Request.Files["registerfile"];
            var status = HttpContext.Current.Request.Form["status"];
            var createby = User.Identity.GetUserName();
            var createdate = DateTime.Today;
            var old_file = HttpContext.Current.Request.Form["file_old"];


            var empInDb = _context.Registerstudents.SingleOrDefault(c => c.id == id);

            string photoName = "";
            if (registerfile != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(registerfile.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(registerfile.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(registerfile.FileName)
                    ));
                var fileSavePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                registerfile.SaveAs(fileSavePath);


                //Delete OldPhoto
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), empInDb.registerfile);

                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }
                var registerDto = new registerstudentDto()
                {
                    id = id,
                    registerid = Int32.Parse(registerid),
                    date = DateTime.Parse(date),
                    studentid = Int32.Parse(studentid),
                    userid = User.Identity.GetUserId(),
                    shiftid = Int32.Parse(shiftid),
                    languageid = Int32.Parse(languageid),
                    periodid = Int32.Parse(periodid),
                    gradeid = Int32.Parse(gradeid),
                    registerfile = photoName,
                    status = status,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(registerDto, empInDb);
                _context.SaveChanges();


            }
            else
            {

                var registerdto = new registerstudentDto()
                {
                    id = id,
                    registerid = Int32.Parse(registerid),
                    date = DateTime.Parse(date),
                    studentid = Int32.Parse(studentid),
                    userid = User.Identity.GetUserId(),
                    shiftid = Int32.Parse(shiftid),
                    languageid = Int32.Parse(languageid),
                    periodid = Int32.Parse(periodid),
                    gradeid = Int32.Parse(gradeid),
                    registerfile = old_file,
                    status = status,
                    createby = createby,
                    createdate = createdate
                };
                Mapper.Map(registerdto, empInDb);
                _context.SaveChanges();

            }
            return Ok(new { });

        }


        //DELETE : /api/Parents/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id)
        {
            var ParentInDb = _context.Registerstudents.SingleOrDefault(c => c.id == id);
            if (ParentInDb == null)
                return BadRequest();

            _context.Registerstudents.Remove(ParentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }

}