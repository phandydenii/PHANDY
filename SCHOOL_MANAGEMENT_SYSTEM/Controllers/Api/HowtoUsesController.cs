using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Data.Entity;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class HowtoUsesController : ApiController
    {
        private ApplicationDbContext _context;

        public HowtoUsesController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }        

        [HttpGet]
        // GET: api/HowtoUses
        public IHttpActionResult GetHowtoUse()
        {
            var getHowtoUse = _context.HowtoUses.Include(c => c.employee).ToList().Select(Mapper.Map<HowtoUse, HowtoUseDto>);
            return Ok(getHowtoUse);
        }

        [HttpGet]
        //GET : api/HowtoUses/{id}
        public IHttpActionResult GetHowtoUseById(int id)
        {
            var getHowtoUseById = _context.HowtoUses.Include(c => c.employee).SingleOrDefault(c => c.id == id);

            if (getHowtoUseById == null)
                return BadRequest();

            return Ok(Mapper.Map<HowtoUse, HowtoUseDto>(getHowtoUseById));
        }

        [HttpPost]


        //POST : api/HowtoUses
        public IHttpActionResult CreateHowtoUse()
        {
            var id = HttpContext.Current.Request.Form["id"];
            var note = HttpContext.Current.Request.Form["note"];
            var employeeid = HttpContext.Current.Request.Form["employeeid"];
            var attachfile = HttpContext.Current.Request.Files["attachfile"];

            //Check if not null
            //var isExist = _context.HowtoUses.SingleOrDefault(c => c.HowtoUseName == HowtoUseName && c.IsDeleted == false);
            //if (isExist != null)
            //  return BadRequest();           


            string fileName = "";

            if (attachfile != null)
            {
                fileName = Path.Combine(Path.GetDirectoryName(attachfile.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(attachfile.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(attachfile.FileName)
                    ));
                var savePath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFile"), fileName);
                attachfile.SaveAs(savePath);
            }
            var HowtoUseDtos = new HowtoUseDto()
            {
                //Id = Int32.Parse(id),
                employeeid = Int32.Parse(employeeid),
                note = note,
                attachfile = fileName
            };
            var HowtoUse = Mapper.Map<HowtoUseDto, HowtoUse>(HowtoUseDtos);
            _context.HowtoUses.Add(HowtoUse);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + HowtoUseDtos.id), HowtoUseDtos);
        }

        //PUT : api/HowtoUses/{id}
        [HttpPut]
        public IHttpActionResult UpdateHowtoUse()
        {
            var id = HttpContext.Current.Request.Form["id"];
            var note = HttpContext.Current.Request.Form["note"];
            var employeeid = HttpContext.Current.Request.Form["employeeid"];
            var attachfile = HttpContext.Current.Request.Files["attachfile"];

            int HowtoUseId = 0;
            if (id != null)
            {
                HowtoUseId = int.Parse(id);
            }

            var isExist = _context.HowtoUses.SingleOrDefault(c => c.id == HowtoUseId); //&& c.IsDeleted == false
            //if (isExist != null)
            //    return BadRequest();

            string fileName = "";

            if (attachfile != null)
            {


                fileName = Path.Combine(Path.GetDirectoryName(attachfile.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(attachfile.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(attachfile.FileName)
                    ));
                var photoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFile"), fileName);
                attachfile.SaveAs(photoPath);

                //Delete Old Photo path
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/UploadFile"), isExist.attachfile);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }

                var HowtoUseDtos = new HowtoUseDto()
                {
                    id = Int32.Parse(id),
                    employeeid = Int32.Parse(employeeid),
                    note = note,
                    attachfile = fileName
                };

                Mapper.Map(HowtoUseDtos, isExist);
                _context.SaveChanges();
            }
            else
            {
                var HowtoUseDtos = new HowtoUseDto()
                {
                    id = Int32.Parse(id),
                    employeeid = Int32.Parse(employeeid),
                    note = note,
                    attachfile = fileName
                };

                Mapper.Map(HowtoUseDtos, isExist);
                _context.SaveChanges();
            }


            return Ok(new { });
        }

        //DELETE : api/HowtoUses/{id}
        public IHttpActionResult DeleteHowtoUse(int id)
        {
            var HowtoUseInDb = _context.HowtoUses.SingleOrDefault(c => c.id == id);
            if (HowtoUseInDb == null)
                return NotFound();
            _context.HowtoUses.Remove(HowtoUseInDb);

            _context.SaveChanges();


            //var photoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/attachfiles"), HowtoUseInDb.Photo);
            //if(File.Exists(photoPath))
            //{
            //    File.Delete(photoPath);
            //}


            return Ok(new { });
        }
    }
}
