using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DGW_LP.Startup))]
namespace DGW_LP
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
