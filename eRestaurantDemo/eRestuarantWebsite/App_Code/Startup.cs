using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(eRestuarantWebsite.Startup))]
namespace eRestuarantWebsite
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
