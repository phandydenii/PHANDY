using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SCHOOL_MANAGEMENT_SYSTEM.Startup))]
namespace SCHOOL_MANAGEMENT_SYSTEM
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
