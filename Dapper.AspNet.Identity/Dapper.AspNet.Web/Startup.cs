using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Dapper.AspNet.Web.Startup))]
namespace Dapper.AspNet.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
