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
    public class FeedbacksController : ApiController
    {
        private ApplicationDbContext _context;

        public FeedbacksController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        // GET: api/Feedbacks
        public IHttpActionResult GetFeedback(int id)
        {
            var getFeedback = _context.Feedbacks.Include(c => c.customer).ToList().Select(Mapper.Map<Feedback, FeedbackDto>).Where(c => c.customerid == id);
            return Ok(getFeedback);
        }

        [HttpGet]
        //GET : api/Feedbacks/{id}
        public IHttpActionResult GetFeedbackById(int id, string a)
        {
            var getFeedbackById = _context.Feedbacks.Include(c => c.customer).SingleOrDefault(c => c.id == id);

            if (getFeedbackById == null)
                return BadRequest();

            return Ok(Mapper.Map<Feedback, FeedbackDto>(getFeedbackById));
        }

        //POST : api/Feedbacks
        public IHttpActionResult CreateFeedback()
        {
            var id = HttpContext.Current.Request.Form["id"];
            var date = HttpContext.Current.Request.Form["date"];
            var customerid = HttpContext.Current.Request.Form["customerid"];
            var comment = HttpContext.Current.Request.Form["comment"];
            var status = HttpContext.Current.Request.Form["status"];
            var image = HttpContext.Current.Request.Files["image"];

            //Check if not null
            //var isExist = _context.Feedbacks.SingleOrDefault(c => c.FeedbackName == FeedbackName && c.IsDeleted == false);
            //if (isExist != null)
            //  return BadRequest();

            string photoName = "";

            if (image != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(image.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(image.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(image.FileName)
                    ));
                var savePath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                image.SaveAs(savePath);
            }
            var FeedbackDtos = new FeedbackDto()
            {
                //Id = Int32.Parse(id),
                customerid = Int32.Parse(customerid),
                date = DateTime.Parse(date),
                comment = comment,
                status = true,
                image = photoName
            };
            var Feedback = Mapper.Map<FeedbackDto, Feedback>(FeedbackDtos);
            _context.Feedbacks.Add(Feedback);
            _context.SaveChanges();

            return Created(new Uri(Request.RequestUri + "/" + FeedbackDtos.id), FeedbackDtos);
        }

        //PUT : api/Feedbacks/{id}
        [HttpPut]
        public IHttpActionResult UpdateFeedback()
        {
            var id = HttpContext.Current.Request.Form["id"];
            var date = HttpContext.Current.Request.Form["date"];
            var customerid = HttpContext.Current.Request.Form["customerid"];
            var comment = HttpContext.Current.Request.Form["comment"];
            var status = HttpContext.Current.Request.Form["status"];
            var image = HttpContext.Current.Request.Files["image"];

            int FeedbackId = 0;
            if (id != null)
            {
                FeedbackId = int.Parse(id);
            }

            var isExist = _context.Feedbacks.SingleOrDefault(c => c.id == FeedbackId); //&& c.IsDeleted == false
            //if (isExist != null)
            //    return BadRequest();

            string photoName = "";

            if (image != null)
            {
                photoName = Path.Combine(Path.GetDirectoryName(image.FileName)
                    , string.Concat(Path.GetFileNameWithoutExtension(image.FileName)
                    , DateTime.Now.ToString("_yyyy_MM_dd_HH_mm_ss")
                    , Path.GetExtension(image.FileName)
                    ));
                var photoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), photoName);
                image.SaveAs(photoPath);

                //Delete Old Photo path
                var oldPhotoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), isExist.image);
                if (File.Exists(oldPhotoPath))
                {
                    File.Delete(oldPhotoPath);
                }

                var FeedbackDtos = new FeedbackDto()
                {
                    id = Int32.Parse(id),
                    customerid = Int32.Parse(customerid),
                    date = DateTime.Parse(date),
                    comment = comment,
                    status = true,
                    image = photoName
                };

                Mapper.Map(FeedbackDtos, isExist);
                _context.SaveChanges();
            }
            else
            {
                var FeedbackDtos = new FeedbackDto()
                {
                    id = Int32.Parse(id),
                    customerid = Int32.Parse(customerid),
                    date = DateTime.Parse(date),
                    comment = comment,
                    status = true,
                    image = photoName
                };

                Mapper.Map(FeedbackDtos, isExist);
                _context.SaveChanges();
            }


            return Ok(new { });
        }

        //DELETE : api/Feedbacks/{id}
        public IHttpActionResult DeleteFeedback(int id)
        {
            var FeedbackInDb = _context.Feedbacks.SingleOrDefault(c => c.id == id);
            if (FeedbackInDb == null)
                return NotFound();
            _context.Feedbacks.Remove(FeedbackInDb);

            _context.SaveChanges();


            //var photoPath = Path.Combine(HttpContext.Current.Server.MapPath("~/Images"), FeedbackInDb.Photo);
            //if(File.Exists(photoPath))
            //{
            //    File.Delete(photoPath);
            //}


            return Ok(new { });
        }
    }
}
