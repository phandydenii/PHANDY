using AutoMapper;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;
using System.Web;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class PaySlipsController : ApiController
    {
        private ApplicationDbContext _context;

        public PaySlipsController()
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
            var getBuilding = _context.PaySlip.Include(s => s.staff).ToList();
            return Ok(getBuilding);
        }

        [HttpGet]
        //Get : api/Buildings
        public IHttpActionResult GetBuilding(int id)
        {
            var getBuilding = _context.PaySlip.Include(s => s.staff).Where(p=>p.id==id).SingleOrDefault();
            return Ok(getBuilding);
        }
        
        [HttpPost]
        public IHttpActionResult InsertStaff()
        {
            var staffid = HttpContext.Current.Request.Form["staffid"];
            var salary = HttpContext.Current.Request.Form["salary"];
            var vat = HttpContext.Current.Request.Form["vat"];
            var penanty = HttpContext.Current.Request.Form["penanty"];
            var bonus = HttpContext.Current.Request.Form["bonus"];
            var note = HttpContext.Current.Request.Form["note"];
            var salaryamount = HttpContext.Current.Request.Form["salaryamount"];
            var date = DateTime.Today;

            var payslipdDto = new PaySlipDto()
            {
                staffid=int.Parse(staffid),
                salary=decimal.Parse(salary),
                vat=decimal.Parse(vat),
                penanty=decimal.Parse(penanty),
                bonus=decimal.Parse(bonus),
                note=note,
                salaryamount=decimal.Parse(salaryamount),
                date=date,
            };

            var PaySlip = Mapper.Map<PaySlipDto, PaySlip>(payslipdDto);
            _context.PaySlip.Add(PaySlip);
            _context.SaveChanges();
            payslipdDto.id = PaySlip.id;

            
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;            SqlConnection con = new SqlConnection(connectionString);            SqlCommand cmd = new SqlCommand("Select max(id) From PaySlip_V", con);

            Int16 maxid;
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                maxid = Convert.ToInt16(cmd.ExecuteScalar());
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return Ok(maxid);
        }

        [HttpPut]
        public IHttpActionResult UpdateStaff(int id)
        {
            var staffid = HttpContext.Current.Request.Form["staffid"];
            var salary = HttpContext.Current.Request.Form["salary"];
            var vat = HttpContext.Current.Request.Form["vat"];
            var penanty = HttpContext.Current.Request.Form["penanty"];
            var bonus = HttpContext.Current.Request.Form["bonus"];
            var note = HttpContext.Current.Request.Form["note"];
            var salaryamount = HttpContext.Current.Request.Form["salaryamount"];
            var date = DateTime.Today;
            var empInDb = _context.PaySlip.SingleOrDefault(c => c.id == id);
            var payslipdDto = new PaySlipDto()
            {
                id = id,
                staffid = int.Parse(staffid),
                salary = decimal.Parse(salary),
                vat = decimal.Parse(vat),
                penanty = decimal.Parse(penanty),
                bonus = decimal.Parse(bonus),
                note = note,
                salaryamount = decimal.Parse(salaryamount),
                date = date,
            };

            Mapper.Map(payslipdDto, empInDb);
            _context.SaveChanges();
            return Ok();
        }


        [HttpDelete]
        //PUT : /api/Staffs/{id}
        public IHttpActionResult DeleteStaff(int id)
        {
            var StaffInDb = _context.PaySlip.SingleOrDefault(c => c.id == id);
            if (StaffInDb == null)
                return NotFound();
            _context.PaySlip.Remove(StaffInDb);
            _context.SaveChanges();
            return Ok(new { });
        }

    }
}
