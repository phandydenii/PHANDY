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
    public class GuestsController : ApiController
    {
        private ApplicationDbContext _context;

        public GuestsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetGuests()
        {
            var getGuest = _context.Guests.ToList().Select(Mapper.Map<Guest, GuestDto>);
            return Ok(getGuest);
        }

        [HttpGet]
        //Get : api/Floors{id}
        public IHttpActionResult GetGuestById(int id)
        {
            var getGuestById = _context.Guests.SingleOrDefault(c => c.id == id);

            if (getGuestById == null)
                return NotFound();

            return Ok(Mapper.Map<Guest, GuestDto>(getGuestById));
        }

        [HttpPost]
        public IHttpActionResult CreateGuest(GuestDto GuestDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.Guests.SingleOrDefault(c => c.ssn == GuestDtos.ssn);
            if (isExist != null)
                return BadRequest();

            var Guest = Mapper.Map<GuestDto, Guest>(GuestDtos);

            _context.Guests.Add(Guest);
            _context.SaveChanges();

            GuestDtos.id = Guest.id;

            return Created(new Uri(Request.RequestUri + "/" + GuestDtos.id), GuestDtos);
        }

        [HttpPut]
        //PUT : /api/Building/{id}
        public IHttpActionResult EditGuest(int id, GuestDto GuestDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.Guests.SingleOrDefault(c => c.id != GuestDtos.id);
            if (isExist != null)
                return BadRequest();

            var GuestInDb = _context.Guests.SingleOrDefault(c => c.id == id);
            Mapper.Map(GuestDtos, GuestInDb);
            _context.SaveChanges();

            return Ok(GuestDtos);
        }

        [HttpDelete]
        //PUT : /api/Building/{id}
        public IHttpActionResult DeleteGuest(int id)
        {

            var GuestInDb = _context.Guests.SingleOrDefault(c => c.id == id);
            if (GuestInDb == null)
                return NotFound();
            _context.Guests.Remove(GuestInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
