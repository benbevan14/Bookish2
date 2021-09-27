using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Bookish2.Web.Startup))]
namespace Bookish2.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
