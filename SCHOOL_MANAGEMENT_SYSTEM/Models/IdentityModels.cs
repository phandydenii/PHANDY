using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SCHOOL_MANAGEMENT_SYSTEM.Models;

namespace SCHOOL_MANAGEMENT_SYSTEM.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public int BrandId { get; set; }
        public Branch Brand { get; set; }
        public string Sex { get; set; }
        public string FullName { get; set; }
        public bool IsDeleted { get; set; }
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Floor> Floors { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<CheckIn> CheckIns { get; set; }
        public DbSet<Booking> Bookings { get; set; }
        public DbSet<Guest> Guests { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<RoomDetail> RoomDetails { get; set; }
        public DbSet<ExchangeRate> Exchanges { get; set; }
        public DbSet<ElectricUsage> Electrics { get; set; }
        public DbSet<WaterUsage> WaterUsages { get; set; }
        public DbSet <WEPrice> WEPrices { get; set; }
        public DbSet <CheckOut> CheckOuts { get; set; }
        public DbSet <CheckOutDeatil> CheckOutDeatils { get; set; }

        public DbSet<Position> Position { get; set; }    
        public DbSet<Invoice> Invoice { get; set; }
        public DbSet<InvoiceDetail> InvoiceDetail { get; set; }    
        public DbSet<ExpenseType> ExpenseTypes { get; set; }
        public DbSet<OtherExpense> OtherExpenses { get; set; }
        public DbSet<Branch> Branchs { get; set; }
        public DbSet<Employees> Employees { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<Salary> Salary { get; set; }
        public DbSet<PaySlip> PaySlip { get; set; }
        public DbSet<Staff> Staffs { get; set; }
        public DbSet<PayDemage> PayDemages { get; set; }
        public DbSet<WaterElectricUsage> WaterEletricUsages { get; set; }


        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        
    }
}