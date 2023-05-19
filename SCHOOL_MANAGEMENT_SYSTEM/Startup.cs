using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using SCHOOL_MANAGEMENT_SYSTEM.Models;
using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

[assembly: OwinStartupAttribute(typeof(SCHOOL_MANAGEMENT_SYSTEM.Startup))]
namespace SCHOOL_MANAGEMENT_SYSTEM
{
    public partial class Startup
    {
        ApplicationDbContext context = new ApplicationDbContext();
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            createRolse();
            createRolesandUsers();
            auto();
        }

        private void createRolesandUsers()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

            // In Startup iam creating first Admin Role and creating a default Admin User     
            if (roleManager.RoleExists("Admin"))
            {
                var user = new ApplicationUser();
                user.UserName = "phandy010@gmail.com";
                user.BrandId = 1;
                user.Sex = "Male";
                user.FullName = "Phan Dy";
                user.Email = "phandy010@gmail.com";
                string userPWD = "Dyadmin.@930323";
                var chkUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin    
                if (chkUser.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Admin");
                }
            }

            // creating Creating Manager role     
            if (!roleManager.RoleExists("Manage Payment"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Manage Payment";
                roleManager.Create(role);

            }

           
        } 
        private void createRolse()
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
            // creating Creating Employee role
            if (!roleManager.RoleExists("Admin"))
            {
                role.Name = "Admin";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Manage User"))
            {
                role.Name = "Manage User";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Manage Other Expense"))
            {
                role.Name = "Manage Other Expense";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Manage Floor"))
            {
                role.Name = "Manage Floor";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Manae Room Type"))
            {
                role.Name = "Manae Room Type";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Manage Room Item"))
            {
                role.Name = "Manage Room Item";
                roleManager.Create(role);
            }

            if (!roleManager.RoleExists("Block Room"))
            {
                role.Name = "Block Room";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Cancel Booking"))
            {
                role.Name = "Cancel Booking";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Manage Check In"))
            {
                role.Name = "Manage Check In";
                roleManager.Create(role);
            }
            if (!roleManager.RoleExists("Manage CheckOut"))
            {
                role.Name = "Manage CheckOut";
                roleManager.Create(role);
            }
        }
    

        private void auto()
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
        }
    }
}
