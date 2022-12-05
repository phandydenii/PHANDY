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
    public class BuildingsController : ApiController
    {
        private ApplicationDbContext _context;

        public BuildingsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuilding()
        {
            var getBuilding = _context.Buildings.ToList().Select(Mapper.Map<Building, BuildingDto>);
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Floors{id}
        public IHttpActionResult GetFloor(int id)
        {
            var getBuildingById = _context.Buildings.SingleOrDefault(c => c.id == id);

            if (getBuildingById == null)
                return NotFound();

            return Ok(Mapper.Map<Building, BuildingDto>(getBuildingById));
        }

        [HttpPost]
        public IHttpActionResult CreateBuilding(BuildingDto BuildingDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.Buildings.SingleOrDefault(c => c.buildingname == BuildingDtos.buildingname);
            if (isExist != null)
                return BadRequest();

            var Building = Mapper.Map<BuildingDto, Building>(BuildingDtos);

            _context.Buildings.Add(Building);
            _context.SaveChanges();

            BuildingDtos.id = Building.id;

            return Created(new Uri(Request.RequestUri + "/" + BuildingDtos.id), BuildingDtos);
        }

        [HttpPut]
        //PUT : /api/Building/{id}
        public IHttpActionResult EditFloor(int id, BuildingDto BuildingDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.Buildings.SingleOrDefault(c => c.buildingname == BuildingDtos.buildingname);
            if (isExist != null)
                return BadRequest();

            var BuildingInDb = _context.Buildings.SingleOrDefault(c => c.id == id);
            Mapper.Map(BuildingDtos, BuildingInDb);
            _context.SaveChanges();

            return Ok(BuildingDtos);
        }

        [HttpDelete]
        //PUT : /api/Building/{id}
        public IHttpActionResult DeleteBuilding(int id)
        {

            var BuildingInDb = _context.Buildings.SingleOrDefault(c => c.id == id);
            if (BuildingInDb == null)
                return NotFound();
            _context.Buildings.Remove(BuildingInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
