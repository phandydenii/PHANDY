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
    public class ShiftsController : ApiController
    {
        private ApplicationDbContext _context;
        public ShiftsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Branchs  for get all record
        [HttpGet]
        public IHttpActionResult GetShifts()
        {
            var shifts = _context.Shiftes.ToList().Select(Mapper.Map<shifts, shiftDto>);
            return Ok(shifts);
        }

        //GET : /api/Branchs/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetShifts(int id)
        {
            var sh = _context.Shiftes.SingleOrDefault(c => c.shiftid == id);
            if (sh == null)
                return NotFound();

            return Ok(Mapper.Map<shifts, shiftDto>(sh));
        }

        //POS : /api/Branchs   for Insert record
        [HttpPost]
        public IHttpActionResult CreateShifts(shiftDto ShiftDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Shiftes.SingleOrDefault(c => c.shiftname == ShiftDto.shiftname);
            if (isExists != null)
                return BadRequest();

            var shift = Mapper.Map<shiftDto, shifts>(ShiftDto);
            _context.Shiftes.Add(shift);
            shift.createdate = DateTime.Today;
            shift.createby = User.Identity.GetUserName();
            _context.SaveChanges();
            ShiftDto.shiftid = shift.shiftid;

            return Created(new Uri(Request.RequestUri + "/" + ShiftDto.shiftid), ShiftDto);


        }

        //PUT : /api/Branchs/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateShift(int id, shiftDto ShiftDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.Shiftes.SingleOrDefault(c => c.shiftname == ShiftDto.shiftname && c.shiftid != ShiftDto.shiftid);
            if (isExists != null)
                return BadRequest();
            var shiftInDb = _context.Shiftes.SingleOrDefault(c => c.shiftid == id);

            shiftInDb.createdate = DateTime.Today;
            shiftInDb.createby = User.Identity.GetUserName();

            Mapper.Map(ShiftDto, shiftInDb);
            _context.SaveChanges();
            return Ok(ShiftDto);

        }

        //DELETE : /api/Brachs/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBranch(int id)
        {
            var BranchInDb = _context.Shiftes.SingleOrDefault(c => c.shiftid == id);
            if (BranchInDb == null)
                return BadRequest();
            var shiftInStudent = _context.Students.SingleOrDefault(c => c.studentshiftid == id);
            if (shiftInStudent != null)
                return BadRequest();

            _context.Shiftes.Remove(BranchInDb);
            _context.SaveChanges();
            return Ok(new { });


        }


    }
}
