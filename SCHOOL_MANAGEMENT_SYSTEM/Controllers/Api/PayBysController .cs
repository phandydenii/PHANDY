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

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    public class PayBysController : ApiController
    {
        private ApplicationDbContext _context;
        public PayBysController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpGet]
        // GET: api/PayBys
        public IHttpActionResult GetPayBy()
        {
            var getPayBy = _context.PayBys.ToList().Select(Mapper.Map<PayBy, PayByDto>);
            return Ok(getPayBy);
        }

        [HttpGet]
        //GET : api/PayBys/{id}
        public IHttpActionResult GetPayByById(int id)
        {
            var getPayByById = _context.PayBys.SingleOrDefault(c => c.id == id);

            if (getPayByById == null)
                return BadRequest();

            return Ok(Mapper.Map<PayBy, PayByDto>(getPayByById));
        }

        //POST : api/PayBys
        public IHttpActionResult CreatePayBy()
        {
            var id = HttpContext.Current.Request.Form["id"];
            var paymentmethod = HttpContext.Current.Request.Form["paymentmethod"];
            var paydate = HttpContext.Current.Request.Form["paybydate"];
            var note = HttpContext.Current.Request.Form["note"];
            var screenshot = HttpContext.Current.Request.Files["screenshot"];

            //Check if not null
            //var isExist = _context.PayBys.SingleOrDefault(c => c.PayByName == PayByName && c.IsDeleted == false);
            //if (isExist != null)
            //  return BadRequest();

            string photoName = "";

            if (screenshot != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(screenshot.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(screenshot.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(screenshot.FileName)
                    ));
                var savePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                screenshot.SaveAs(savePath);
            }
            var PayByDtos = new PayByDto()
            {
                //Id = Int32.Parse(id),
                paymentmethod = paymentmethod,
                paybydate = DateTime.Parse(paydate),
                note = note,
                screenshot = photoName
            };
            var PayBy = Mapper.Map<PayByDto, PayBy>(PayByDtos);
            _context.PayBys.Add(PayBy);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + PayByDtos.id), PayByDtos);
        }

        //PUT : api/PayBys/{id}
        [HttpPut]
        public IHttpActionResult UpdatePayBy()
        {
            var id = HttpContext.Current.Request.Form["id"];
            var paymentmethod = HttpContext.Current.Request.Form["paymentmethod"];
            var paydate = HttpContext.Current.Request.Form["paybydate"];
            var note = HttpContext.Current.Request.Form["note"];
            var screenshot = HttpContext.Current.Request.Files["screenshot"];

            int PayById = 0;
            if (id != null)
            {
                PayById = int.Parse(id);
            }

            var isExist = _context.PayBys.SingleOrDefault(c => c.id == PayById); //&& c.IsDeleted == false
            //if (isExist != null)
            //    return BadRequest();

            string photoName = "";

            if (screenshot != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(screenshot.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(screenshot.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(screenshot.FileName)
                    ));
                var photoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                screenshot.SaveAs(photoPath);

                //Delete Old Photo path
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), isExist.screenshot);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }

                var PayByDtos = new PayByDto()
                {
                    id = Int32.Parse(id),
                    paymentmethod = paymentmethod,
                    paybydate = DateTime.Parse(paydate),
                    note = note,
                    screenshot = photoName
                };

                Mapper.Map(PayByDtos, isExist);
                _context.SaveChanges();
            }
            else
            {
                var PayByDtos = new PayByDto()
                {
                    id = Int32.Parse(id),
                    paymentmethod = paymentmethod,
                    paybydate = DateTime.Parse(paydate),
                    note = note,
                    screenshot = photoName
                };

                Mapper.Map(PayByDtos, isExist);
                _context.SaveChanges();
            }


            return Ok(new { });
        }

        //DELETE : api/PayBys/{id}
        public IHttpActionResult DeletePayBy(int id)
        {
            var PayByInDb = _context.PayBys.SingleOrDefault(c => c.id == id);
            if (PayByInDb == null)
                return NotFound();
            _context.PayBys.Remove(PayByInDb);

            _context.SaveChanges();


            //var photoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), PayByInDb.Photo);
            //if(File.Exists(photoPath))
            //{
            //    File.Delete(photoPath);
            //}


            return Ok(new { });
        }
    }
}
