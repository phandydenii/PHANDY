using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System.Configuration;
using System.Data.SqlClient;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    public class BookingController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        private ApplicationDbContext _context;

        public BookingController()
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
            SqlDataAdapter adp = new SqlDataAdapter("select id,roomid from booking_tbl where expiredate=FORMAT (getdate(), 'yyyy-MM-dd') and status='Active'", conx);
                
            adp.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    var id = row["id"].ToString();
                    var roomid= row["roomid"].ToString();

                    SqlCommand cmd = new SqlCommand("update booking_tbl set status='Expire' where id=" + int.Parse(id), conx);
                    SqlCommand cmd1 = new SqlCommand("update room_tbl set status='FREE' where id=" + int.Parse(roomid), conx);
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

            var roomViewModel = new RoomViewModel()
            {
                Rooms = _context.Rooms.ToList(),
                GuestList = _context.Guests.ToList(),
                ListRoomBook = _context.Rooms.Where(c => c.status == "BOOK").ToList(),
                ListRoomFree = _context.Rooms.Where(c => c.status == "FREE").ToList(),
            };

            return View(roomViewModel);
        }


    }
}
