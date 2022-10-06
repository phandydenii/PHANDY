using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{

    [Authorize]
    public class LocationController : ApiController
    {
        private ApplicationDbContext _context;
        public LocationController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        //GET : /api/Departments  for get all record
        [HttpGet]
        public IHttpActionResult GetLocation()
        {
            var deparment = _context.Location.ToList().Select(Mapper.Map<Location, LocationDto>).Where(c=>c.status==true);
            return Ok(deparment);
        }

        //GET : /api/Departments/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetLocation(int id)
        {
            var deparment = _context.Location.SingleOrDefault(c => c.id == id);
            if (deparment == null)
                return NotFound();

            return Ok(Mapper.Map<Location, LocationDto>(deparment));
        }

        //POS : /api/Departments   for Insert record
        [HttpPost]
        public IHttpActionResult CreateLocation(LocationDto locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Departments.SingleOrDefault(c => c.Name == locationDto.location);
            if (isExists != null)
                return BadRequest();
            
            var department = Mapper.Map<LocationDto, Location>(locationDto);
            department.status = true;
            _context.Location.Add(department);
            _context.SaveChanges();
            locationDto.id = department.id;

            return Created(new Uri(Request.RequestUri + "/" + locationDto.id), locationDto);


        }

        //PUT : /api/Departments/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateLocation(int id, LocationDto locationDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Location.SingleOrDefault(c => c.location == locationDto.location);
            if (isExists != null)
                return BadRequest();

            var DepartmentInDb = _context.Location.SingleOrDefault(c => c.id == id);
            //DepartmentInDb.status= true;
            locationDto.status = true;
            Mapper.Map(locationDto, DepartmentInDb);
            _context.SaveChanges();
            return Ok(locationDto);

        }

        //DELETE : /api/Departments/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteLocation(int id)
        {
            var DepartmentInDb = _context.Location.SingleOrDefault(c => c.id == id);
            if (DepartmentInDb == null)
                return BadRequest();

            var isExists = _context.InvoiceDetail.Where(c => c.locationid == id).Take(1);
            if (isExists.Count() > 0)
                return BadRequest();


            _context.Location.Remove(DepartmentInDb);
            _context.SaveChanges();
            return Ok(new { });


        }

    }
}
