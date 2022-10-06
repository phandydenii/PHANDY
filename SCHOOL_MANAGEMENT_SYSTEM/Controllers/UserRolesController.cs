using SCHOOL_MANAGEMENT_SYSTEM.Models;
using SCHOOL_MANAGEMENT_SYSTEM.ViewModels;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers
{
    [Authorize]
    public class UserRolesController : Controller
    {
        private ApplicationDbContext _context;
        private ApplicationUserManager _userManager;

        public UserRolesController()
        {
            _context = new ApplicationDbContext();
        }

        public UserRolesController(ApplicationUserManager userManager)
        {
            UserManager = userManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: UserRoles
        [Route("manage-user-roles")]
        public ActionResult Index()
        {
            var users = _context.Users.Where(i=>i.IsDeleted==false).ToList();

            return View(users);
        }

        // GET: /users/{id}
        public ActionResult GetUser(string id)
        {
            var roleStore = new RoleStore<IdentityRole>(new ApplicationDbContext());
            var roleManager = new RoleManager<IdentityRole>(roleStore);

            var user = _context.Users.Include(c => c.Roles).Where(i=>i.IsDeleted==false).SingleOrDefault(c => c.Id == id);

            var roles = roleManager;

            var viewModel = new UserRoleViewModel()
            {
                User = user,
                Role = roles
            };

            return View("AssignRoles", viewModel);
        }

        [HttpGet]
        public ActionResult EditUser(string id)
        {
            var user = _context.Users.SingleOrDefault(c => c.Id == id);

            if (user == null)
                return HttpNotFound();

            var viewModel = new RegisterViewModel()
            {
                Id = user.Id,
                Email = user.Email,
                FullName=user.FullName,
                Sex=user.Sex,
                
                
            };

            return View("EditUser", viewModel);
        }

        public ActionResult DeleteUser(string id)
        {
            var user = UserManager.FindById(id);
            user.IsDeleted = true;
            //UserManager.Delete(user);
            UserManager.Update(user);
            return RedirectToAction("Index", "UserRoles");

        }
    }
}