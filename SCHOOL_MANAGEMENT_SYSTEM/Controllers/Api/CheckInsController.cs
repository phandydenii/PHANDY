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
    public class CheckInsController : ApiController
    {
        private ApplicationDbContext _context;

        public CheckInsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        [HttpGet]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckIn()
        {
            var getBuilding = _context.CheckIns.ToList().Select(Mapper.Map<CheckIn, CheckInDto>);
            return Ok(getBuilding);

        }
        [HttpGet]
        //Get : api/CheckIns
        public IHttpActionResult GetCheckInById(int id)
        {
            var getBuildingById = _context.CheckIns.SingleOrDefault(c => c.id == id);

            if (getBuildingById == null)
                return NotFound();

            return Ok(Mapper.Map<CheckIn, CheckInDto>(getBuildingById));

        }
        [HttpPost]
        //Get : api/CheckIns
        public IHttpActionResult CreateCheckIn(CheckInDto CheckInDto)
        {

            if (!ModelState.IsValid)
                return BadRequest();

            //var isExist = _context.Vacancies.SingleOrDefault(c => c.VacancyName == VacancyDtos.VacancyName);
            //if (isExist != null)
            //    return BadRequest();

            var CheckInInDb = Mapper.Map<CheckInDto, CheckIn>(CheckInDto);
            _context.CheckIns.Add(CheckInInDb);
            _context.SaveChanges();

            CheckInInDb.id = CheckInInDb.id;

            return Created(new Uri(Request.RequestUri + "/" + CheckInDto.id), CheckInDto);


        }
        [HttpPut]

        public IHttpActionResult UpdateUser(int id, CheckInDto CheckInDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExist = _context.Vacancies.SingleOrDefault(c => c.VacancyName == VacancyDtos.VacancyName);
            //if (isExist != null)
            //    return BadRequest();
            var CheckIninDb = _context.CheckIns.SingleOrDefault(c => c.id == id);
            Mapper.Map(CheckInDto, CheckIninDb);
            _context.SaveChanges();

            return Ok(CheckIninDb);

        }

        //DELETE : api/Companies/{id}
        public IHttpActionResult DeleteUser(int id)
        {
            var bookinfInDb = _context.CheckIns.SingleOrDefault(c => c.id == id);
            if (bookinfInDb == null)
                return NotFound();
            _context.CheckIns.Remove(bookinfInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
