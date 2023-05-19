using AutoMapper;
using Microsoft.AspNet.Identity;
using SCHOOL_MANAGEMENT_SYSTEM.Dtos;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Helpers;
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
            DataTable dt = new DataTable();
            var connectionString = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conx = new SqlConnection(connectionString);
            SqlDataAdapter adp = new SqlDataAdapter("select booking_tbl.id,roomid,guest_tbl.id as guestid from booking_tbl inner join guest_tbl on guest_tbl.id=booking_tbl.guestid where expiredate<=FORMAT (getdate(), 'yyyy-MM-dd') and guest_tbl.status='Book' and booking_tbl.status='Book'", conx);

            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var id = row["id"].ToString();
                    var roomid = row["roomid"].ToString();
                    var guestid = row["guestid"].ToString();

                    SqlCommand cmd = new SqlCommand("update booking_tbl set status='Expire' where id=" + int.Parse(id), conx);
                    SqlCommand cmd1 = new SqlCommand("update room_tbl set status='FREE' where id=" + int.Parse(roomid), conx);
                    SqlCommand cmd2 = new SqlCommand("update guest_tbl set status='Expire' where id=" + int.Parse(guestid), conx);
                    try
                    {
                        conx.Open();
                        cmd.ExecuteNonQuery();
                        cmd1.ExecuteNonQuery();
                        cmd2.ExecuteNonQuery();
                        conx.Close();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }

            }

            var countModel = new CountModel()
            {
                TotalUser = _context.Users.Count(),
                TotalExpense = _context.Exchanges.Count(),
                TotalGuest = _context.Guests.Count(),
                TotalStaff = _context.Staffs.Count(),
                TotalItem = _context.Items.Count(),
                TotalcheckIn = _context.CheckIns.Count(),
                TotalRoom=_context.Rooms.Count(),
                TotalBook=_context.Bookings.Count(),
            };
            return View(countModel);
        }
    }
}