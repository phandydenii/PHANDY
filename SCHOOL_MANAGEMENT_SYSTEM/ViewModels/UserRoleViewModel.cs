using SCHOOL_MANAGEMENT_SYSTEM.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace SCHOOL_MANAGEMENT_SYSTEM.ViewModels
{
    public class UserRoleViewModel
    {
        public ApplicationUser User { get; set; }
        public RoleManager<IdentityRole> Role { get; set; }
    }

    public class Roles
    {
        public string RoleId { get; set; }

        public string RoleName { get; set; }
    }
}