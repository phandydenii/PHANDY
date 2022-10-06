using AutoMapper;
using Microsoft.AspNet.Identity;
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
    public class EmergencysController : ApiController
    {
        private ApplicationDbContext _context;
        public EmergencysController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/GetSalarys  for get all record
        [HttpGet]
        public IHttpActionResult GetEmergencys()
        {
            var salarys = _context.Emergencys.ToList().Select(Mapper.Map<emergency, emergencyDto>);
            return Ok(salarys);
        }

        //GET : /api/GetSalarys/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetEmergencys(int id)
        {
            var salarys = _context.Emergencys.SingleOrDefault(c => c.emerid == id);
            if (salarys == null)
                return NotFound();

            return Ok(Mapper.Map<emergency, emergencyDto>(salarys));
        }

        //POS : /api/GetSalarys   for Insert record
        [HttpPost]
        public IHttpActionResult CreateEmergencys(emergencyDto EmergencyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var emers = Mapper.Map<emergencyDto, emergency>(EmergencyDto);
            _context.Emergencys.Add(emers);
            emers.createby = User.Identity.GetUserName();
            emers.createdate = DateTime.Now;
            _context.SaveChanges();
            EmergencyDto.emerid = emers.emerid;

            return Created(new Uri(Request.RequestUri + "/" + EmergencyDto.emerid), EmergencyDto);


        }

        //PUT : /api/GetSalarys/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateEmergencys(int id, emergencyDto EmergencyDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            //var isExists = _context.Salarys.SingleOrDefault(c => c.Name == BranchDto.Name && c.Id != BranchDto.Id);
            //if (isExists != null)
            //    return BadRequest();
            var emerInDb = _context.Emergencys.SingleOrDefault(c => c.emerid == id);
            Mapper.Map(EmergencyDto, emerInDb);
            emerInDb.createby = User.Identity.GetUserName();
            emerInDb.createdate = DateTime.Now;
            _context.SaveChanges();
            return Ok(EmergencyDto);

        }

        //DELETE : /api/GetSalarys/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteEmergencys(int id)
        {
            var emerInDb = _context.Emergencys.SingleOrDefault(c => c.emerid == id);
            if (emerInDb == null)
                return BadRequest();

            _context.Emergencys.Remove(emerInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
