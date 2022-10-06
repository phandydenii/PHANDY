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
    public class BankAccountsController : ApiController
    {
        private ApplicationDbContext _context;

        public BankAccountsController()
        {
            _context = new ApplicationDbContext();

        }
        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }

        [HttpGet]
        //Get InvoiceDetail by invoiceid
        public IHttpActionResult GetBankAccount(int id)
        {
            var BankAccount = _context.BankAccounts.Include(c => c.customer).ToList().Select(Mapper.Map<BankAccount, BankAccountDto>).Where(c => c.customerid  == id);
            return Ok(BankAccount);
        }


        //GET : /api/BankAccounts/{id} for get record by id
        [HttpGet]
        public IHttpActionResult GetBankAccount(int id, string a)
        {
            var inv = _context.BankAccounts.Include(c => c.customer).SingleOrDefault(c => c.id == id);
            if (inv == null)
                return NotFound();

            return Ok(Mapper.Map<BankAccount, BankAccountDto>(inv));

        }

        //POS : /api/BankAccount   for Insert record
        [HttpPost]
        public IHttpActionResult CreateBankAccount(BankAccountDto BankAccountDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var BankAccountInDb = Mapper.Map<BankAccountDto, BankAccount>(BankAccountDto);
            BankAccountInDb.date = BankAccountDto.date;
            BankAccountInDb.customerid = BankAccountDto.customerid;
            BankAccountInDb.abaname = BankAccountDto.abaname;
            BankAccountInDb.abanumber = BankAccountDto.abanumber;
            BankAccountInDb.status = true; 
            _context.BankAccounts.Add(BankAccountInDb);
            _context.SaveChanges();
            BankAccountDto.id = BankAccountInDb.id;
            return Created(new Uri(Request.RequestUri + "/" + BankAccountDto.id), BankAccountDto);

        }

        //PUT : /api/BankAccounts/{id}  for Update record
        [HttpPut]
        public IHttpActionResult UpdateBankAccount(int id, BankAccount BankAccount)
        {
            string UserId = User.Identity.GetUserId();
            if (!ModelState.IsValid)
                return BadRequest();

            var BankAccountInDb = _context.BankAccounts.SingleOrDefault(c => c.id == id);
            BankAccountInDb.id = BankAccount.id;
            BankAccountInDb.abaname = BankAccount.abaname;
            BankAccountInDb.customerid = BankAccount.customerid;
            BankAccountInDb.date = BankAccount.date;
            BankAccountInDb.abanumber = BankAccount.abanumber;
            _context.SaveChanges();
            return Ok(BankAccount);
        }



        //DELETE : /api/BankAccounts/{id}  for Delete record
        [HttpDelete]
        public IHttpActionResult DeleteBankAccount(int id)
        {


            var BankAccountInDb = _context.BankAccounts.SingleOrDefault(c => c.id == id);
            if (BankAccountInDb == null)
                return BadRequest();

            _context.BankAccounts.Remove(BankAccountInDb);
            _context.SaveChanges();
            return Ok(new { });


        }
    }
}
