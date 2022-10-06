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
    public class StudyperiodsController : ApiController
    {
        private ApplicationDbContext _context;
        public StudyperiodsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        //GET : /api/Branchs  for get all record
        [HttpGet]
        public IHttpActionResult GetPeriod()
        {
            var shifts = _context.studyperiods.ToList().Select(Mapper.Map<studyperiod, studyperiodDto>);
            return Ok(shifts);
        }

        //GET : /api/Branchs/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetPeriod(int id)
        {
            var sh = _context.studyperiods.SingleOrDefault(c => c.studyperiodid == id);
            if (sh == null)
                return NotFound();

            return Ok(Mapper.Map<studyperiod, studyperiodDto>(sh));
        }

        //POS : /api/Branchs   for Insert record
        [HttpPost]
        public IHttpActionResult CreatePeriod(studyperiodDto studyperiodDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.studyperiods.SingleOrDefault(c => c.period == studyperiodDto.period);
            if (isExists != null)
                return BadRequest();

            var shift = Mapper.Map<studyperiodDto, studyperiod>(studyperiodDto);
            _context.studyperiods.Add(shift);
            shift.createdate = DateTime.Today;
            shift.createby = User.Identity.GetUserName();
            shift.periodstatus = "active";
            _context.SaveChanges();
            studyperiodDto.studyperiodid = shift.studyperiodid;

            return Created(new Uri(Request.RequestUri + "/" + studyperiodDto.studyperiodid), studyperiodDto);


        }

        //PUT : /api/Branchs/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdatePeriod(int id, studyperiodDto ShiftDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExists = _context.studyperiods.SingleOrDefault(c => c.period == ShiftDto.period && c.studyperiodid != ShiftDto.studyperiodid);
            if (isExists != null)
                return BadRequest();
            var shiftInDb = _context.studyperiods.SingleOrDefault(c => c.studyperiodid == id);

            shiftInDb.createdate = DateTime.Today;
            shiftInDb.createby = User.Identity.GetUserName();
            shiftInDb.periodstatus = "active";
            Mapper.Map(ShiftDto, shiftInDb);
            _context.SaveChanges();
            return Ok(ShiftDto);

        }

        //DELETE : /api/Brachs/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeletePeriod(int id)
        {
            var BranchInDb = _context.studyperiods.SingleOrDefault(c => c.studyperiodid == id);
            if (BranchInDb == null)
                return BadRequest();

            var used = _context.Registerstudents.SingleOrDefault(c => c.periodid == id);
            if (used != null)
                return BadRequest();

            _context.studyperiods.Remove(BranchInDb);
            _context.SaveChanges();
            return Ok(new { });


        }


    }
}
