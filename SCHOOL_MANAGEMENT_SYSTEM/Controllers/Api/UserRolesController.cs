using SCHOOL_MANAGEMENT_SYSTEM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using System.Net.Http;
using System.Threading.Tasks;
using System.Data.Entity;

namespace SCHOOL_MANAGEMENT_SYSTEM.Controllers.Api
{
    [Authorize]
    [AllowAnonymous]
    public class UserRolesController : ApiController
    {
        private ApplicationDbContext _context;
        private UserManager<ApplicationUser> _userManager;

        public UserRolesController()
        {
            _context = new ApplicationDbContext();

            var store = new UserStore<ApplicationUser>(_context);

            _userManager = new UserManager<ApplicationUser>(store);
        }

        [HttpDelete]
        [Route("api/users/{userId}/roles/{roleName}")]
        public async Task<IHttpActionResult> RemoveFromRole(string userId, string roleName)
        {
            try
            {
                var role = _context.Roles.SingleOrDefault(c => c.Name == roleName);

                await _context.Database.ExecuteSqlCommandAsync("DELETE FROM AspNetUserRoles WHERE UserId='" + userId + "' AND RoleId='" + role.Id + "'");

                _context.SaveChanges();

                return Ok(new { });
            }
            catch (Exception ex)
            {
                return BadRequest();
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/users/{userId}/roles/{roleName}")]
        public async Task<IHttpActionResult> AddToRole(string userId, string roleName)
        {
            await _userManager.AddToRoleAsync(userId, roleName);

            return Ok(new { });
        }
    }
}
