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
    public class ItemsController : ApiController
    {
        private ApplicationDbContext _context;

        public ItemsController()
        {
            _context = new ApplicationDbContext();
        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get : api/Items
        public IHttpActionResult GetItem()
        {
            var getItem = _context.Items.ToList().Select(Mapper.Map<Item, ItemDto>);

            return Ok(getItem);
        }


        [HttpGet]
        //Get : api/Items{id}
        public IHttpActionResult GetItem(int id)
        {
            var getItemById = _context.Items.SingleOrDefault(c => c.id == id);

            if (getItemById == null)
                return NotFound();

            return Ok(Mapper.Map<Item, ItemDto>(getItemById));
        }
        [HttpPost]
        public IHttpActionResult CreateItem(ItemDto ItemDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var isExist = _context.Items.SingleOrDefault(c => c.itemname == ItemDtos.itemname);
            if (isExist != null)
                return BadRequest();

            var Item = Mapper.Map<ItemDto, Item>(ItemDtos);

            _context.Items.Add(Item);
            _context.SaveChanges();

            ItemDtos.id = Item.id;

            return Created(new Uri(Request.RequestUri + "/" + ItemDtos.id), ItemDtos);
        }

        [HttpPut]
        //PUT : /api/Item/{id}
        public IHttpActionResult EditItem(int id, ItemDto ItemDtos)
        {
            if (!ModelState.IsValid)
                return BadRequest();
            var isExist = _context.Items.SingleOrDefault(c => c.itemname == ItemDtos.itemname && c.id != ItemDtos.id);
            if (isExist != null)
                return BadRequest();

            var ItemInDb = _context.Items.SingleOrDefault(c => c.id == id);
            Mapper.Map(ItemDtos, ItemInDb);
            _context.SaveChanges();

            return Ok(ItemDtos);
        }

        [HttpDelete]
        //PUT : /api/Items/{id}
        public IHttpActionResult DeleteItem(int id)
        {

            var ItemInDb = _context.Items.SingleOrDefault(c => c.id == id);
            if (ItemInDb == null)
                return NotFound();
            _context.Items.Remove(ItemInDb);
            _context.SaveChanges();

            return Ok(new { });
        }
    }
}
