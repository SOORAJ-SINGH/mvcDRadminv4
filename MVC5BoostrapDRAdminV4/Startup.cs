using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MVC5BoostrapDRAdminV4.Startup))]
namespace MVC5BoostrapDRAdminV4
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
