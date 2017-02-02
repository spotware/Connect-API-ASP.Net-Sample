using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Accounts_API_Web.Startup))]
namespace Accounts_API_Web
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
