using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }

        protected override void Dispose(bool disposing)
        {
            _context.Dispose();
        }
        public ActionResult Index()
        {
            DataTable dt1 = new DataTable();
            DataTable dt = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp1 = new SqlDataAdapter("select top 1 id from ExchangeRates where IsDeleted=0 order by id desc", conx);
            SqlDataAdapter adp = new SqlDataAdapter("select checkinid,roomid,guestid,weid from INVOICE_NEW", conx);
            
            adp1.Fill(dt1);
            
            var invoicedate = DateTime.Today;
            var userid = User.Identity.GetUserId();
            var createby = User.Identity.GetUserId();
            var updateby = User.Identity.GetUserId();
            var updatedate = DateTime.Today;
            var createdate = DateTime.Today;
            var exid = int.Parse(dt1.Rows[0][0].ToString());
            var status = "ACTIVE";

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var checkinid = int.Parse(row["checkinid"].ToString());
                    var roomid = int.Parse(row["roomid"].ToString());
                    var guestid = int.Parse(row["guestid"].ToString());
                    var weid = int.Parse(row["weid"].ToString());
                    
                    SqlCommand cmd = new SqlCommand("insert into invoice_tbl (invoicedate,roomid,weusageid,guestid,userid,exchangerateid,createby,createdate,updateby,updatedate,paid,printed,status) values ('"+invoicedate+"','"+roomid+"','"+weid+"','"+guestid+"','"+userid+"','"+exid+"','"+createby+"','"+createdate+"','"+updateby+"','"+updatedate+ "',0,0,'" + status+"')", conx);
                    SqlCommand cmd1 = new SqlCommand("update guest_tbl set status='CHECK-IN' where id=" + guestid, conx);
                    try
                    {
                        conx.Open();
                        cmd.ExecuteNonQuery();
                        cmd1.ExecuteNonQuery();
                        conx.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }


            //var currentUser = User.Identity.GetUserId();
            //var userOne = _context.Users.SingleOrDefault(c => c.Id == currentUser);
            ////var brandId = userOne.BrandId;
            var countModel = new CountModel()
            {
                TotalUser = _context.Users.Count(),
                TotalExpense = _context.Exchanges.Count(),
                TotalGuest = _context.Guests.Count(),
                TotalStaff = _context.Staffs.Count(),
                TotalItem = _context.Items.Count(),
                TotalcheckIn = _context.CheckIns.Count(),
                TotalRoom=_context.Rooms.Count(),
            };


            return View(countModel);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}