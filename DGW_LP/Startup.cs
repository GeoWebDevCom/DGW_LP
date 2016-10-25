using DGW_LP.Models;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DGW_LP.Startup))]
namespace DGW_LP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            //app.CreatePerOwinContext(MyContext.Create);
            //app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);

            ConfigureAuth(app);
        }
    }
}
