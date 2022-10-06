using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Data.Entity;
using System.Web;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class StaffsController : ApiController
    {
        private ApplicationDbContext _context;

        public StaffsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Staff
        public IHttpActionResult GetStaff()
        {
            var getStaff = _context.Staffs.Include(c => c.position).ToList();
            return Ok(getStaff);
            //var getStaff = (from s in _context.Staffs
            //                join p in _context.Position on s.positionid equals p.id
            //                select new StaffV
            //                {
            //                    id = s.id,
            //                    positionname = p.positionname,
            //                    positionnamekh = p.positionnamekh,
            //                    name = s.name,
            //                    namekh = s.namekh,
            //                    sex=s.sex,
            //                    phone=s.phone,
            //                    dob=s.dob,
            //                    address=s.address,
            //                    email=s.email,
            //                    identityno=s.identityno,
            //                    photo=s.photo,
            //                    createby=s.createby,
            //                    createdate=s.createdate,
            //                   status = s.status
            //               }).ToList();

            //return Ok(getStaff);
        }


        [HttpGet]
        //Get : api/RoomTypes{id}
        public IHttpActionResult GetStaffById(int id)
        {
            var getStaffById = _context.Staffs.SingleOrDefault(c => c.id == id);

            if (getStaffById == null)
                return NotFound();

            return Ok(Mapper.Map<Staff, StaffDto>(getStaffById));
        }
        [HttpPost]
        public IHttpActionResult CreateStaff(StaffDto StaffDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.Staffs.SingleOrDefault(c => c.id == StaffDtos.id);
            if (isExist != null)
                return BadRequest();

            var Staff = Mapper.Map<StaffDto, Staff>(StaffDtos);

            _context.Staffs.Add(Staff);
            _context.SaveChanges();

            StaffDtos.id = Staff.id;

            return Created(new Uri(Request.RequestUri + "/" + StaffDtos.id), StaffDtos);
        }

        [HttpPut]
        //PUT : /api/Staff/{id}
        public IHttpActionResult EditStaff(int id, StaffDto StaffDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.Staffs.SingleOrDefault(c => c.id == StaffDtos.id && c.id != StaffDtos.id);
            if (isExist != null)
                return BadRequest();

            var StaffInDb = _context.Staffs.SingleOrDefault(c => c.id == id);
            Mapper.Map(StaffDtos, StaffInDb);
            _context.SaveChanges();

            return Ok(StaffInDb);
        }

        [HttpDelete]
        //PUT : /api/Staffs/{id}
        public IHttpActionResult DeleteStaff(int id)
        {

            var StaffInDb = _context.Staffs.SingleOrDefault(c => c.id == id);
            if (StaffInDb == null)
                return NotFound();
            _context.Staffs.Remove(StaffInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
