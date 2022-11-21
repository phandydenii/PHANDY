using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    public class WaterPoserPriceController : ApiController
    {
        private ApplicationDbContext _context;

        public WaterPoserPriceController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        [Route("api/WaterPoserPrice/{a}/{b}")]
        public IHttpActionResult GetLastExchange(int a, int b)
        {
            var exchageRates = _context.WaterPowerPrices.OrderByDescending(c => c.id).FirstOrDefault(c => c.IsDeleted == false);
            return Ok(exchageRates);
        }
    }
}
