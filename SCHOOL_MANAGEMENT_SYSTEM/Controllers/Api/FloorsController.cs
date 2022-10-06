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
    public class FloorsController : ApiController
    {
        private ApplicationDbContext _context;

        public FloorsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Floors
        public IHttpActionResult GetFloor()
        {
            var getFloor = (from f in _context.Floors
                            join b in _context.Buildings on f.buildingid equals b.id
                            select new FloorV
                            {
                                id = f.id,
                                floor_no = f.floor_no,
                                buildingid = b.id,
                                buildingname = b.buildingname,
                                buildingnamekh = b.buildingnamekh,
                                status = f.status

                            }).ToList();

            return Ok(getFloor);
        }


        [HttpGet]
        //Get : api/Floors{id}
        public IHttpActionResult GetFloor(int id)
        {
            var getFloorById = _context.Floors.SingleOrDefault(c => c.id == id);

            if (getFloorById == null)
                return NotFound();

            return Ok(Mapper.Map<Floor, FloorDto>(getFloorById));
        }
        [HttpPost]
        public IHttpActionResult CreateFloor(FloorDto FloorDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.Floors.SingleOrDefault(c => c.floor_no == FloorDtos.floor_no);
            if (isExist != null)
                return BadRequest();

            var Floor = Mapper.Map<FloorDto, Floor>(FloorDtos);

            _context.Floors.Add(Floor);
            _context.SaveChanges();

            FloorDtos.id = Floor.id;

            return Created(new Uri(Request.RequestUri + "/" + FloorDtos.id), FloorDtos);
        }

        [HttpPut]
        //PUT : /api/Floor/{id}
        public IHttpActionResult EditFloor(int id, FloorDto FloorDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.Floors.SingleOrDefault(c => c.floor_no == FloorDtos.floor_no && c.id != FloorDtos.id);
            if (isExist != null)
                return BadRequest();

            var FloorInDb = _context.Floors.SingleOrDefault(c => c.id == id);
            Mapper.Map(FloorDtos, FloorInDb);
            _context.SaveChanges();

            return Ok(FloorDtos);
        }

        [HttpDelete]
        //PUT : /api/Floors/{id}
        public IHttpActionResult DeleteFloor(int id)
        {

            var FloorInDb = _context.Floors.SingleOrDefault(c => c.id == id);
            if (FloorInDb == null)
                return NotFound();
            _context.Floors.Remove(FloorInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
