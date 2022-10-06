using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;


namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class ConditionsController : ApiController
    {
        private ApplicationDbContext _context;

        public ConditionsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }



        [HttpGet]
        //Get InvoiceDetail by invoiceid
        public IHttpActionResult GetCondition()
        {
            var Condition = _context.Conditions.Include(c => c.employee).ToList().Select(Mapper.Map<Condition, ConditionDto>);
            return Ok(Condition);
        }


        //GET : /api/conditions/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetCondition(int id)
        {
            var inv = _context.Conditions.Include(c => c.employee).SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<Condition, ConditionDto>(inv));

        }

        //POS : /api/condition   for Insert record
        [HttpPost]
        public IHttpActionResult CreateCondition(ConditionDto conditionDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var conditionInDb = Mapper.Map<ConditionDto, Condition>(conditionDto);
                conditionInDb.conditionnote = conditionDto.conditionnote;
                conditionInDb.createby = User.Identity.GetUserName();
                conditionInDb.updatedate = DateTime.Today;
                conditionInDb.updateby = User.Identity.GetUserName();
                conditionInDb.createdate = DateTime.Today;
                _context.Conditions.Add(conditionInDb);
                _context.SaveChanges();
                conditionDto.id = conditionInDb.id;
            return Created(new Uri(Request.RequestUri + "/" + conditionDto.id), conditionDto);

        }

        //PUT : /api/conditions/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateCondition(int id, Condition condition)
        {


            string UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest();


            var conditionInDb = _context.Conditions.SingleOrDefault(c => c.id == id);
            conditionInDb.id = condition.id;
            conditionInDb.employeeid = condition.employeeid;
            conditionInDb.conditionnote = condition.conditionnote;
            conditionInDb.createby = User.Identity.GetUserName(); 
            conditionInDb.updatedate = DateTime.Today;
            conditionInDb.updateby = User.Identity.GetUserName(); 
            conditionInDb.createdate = DateTime.Today;
            _context.SaveChanges();
            return Ok(condition);
        }



        //DELETE : /api/conditions/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteCondition(int id)
        {


            var conditionInDb = _context.Conditions.SingleOrDefault(c => c.id == id);
            if (conditionInDb == null)
                return BadRequest();

            _context.Conditions.Remove(conditionInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
