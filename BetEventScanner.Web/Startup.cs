using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BetEventScanner.Web.Startup))]
namespace BetEventScanner.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
