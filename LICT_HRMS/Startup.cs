using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(LICT_HRMS.Startup))]
namespace LICT_HRMS
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
