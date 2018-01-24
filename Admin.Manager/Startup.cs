using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Admin.Manager.Startup))]
namespace Admin.Manager
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
