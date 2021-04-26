using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WebMooc_beta.Startup))]
namespace WebMooc_beta
{
    public partial class Startup {
        public void Configuration(IAppBuilder app) {
            ConfigureAuth(app);
        }
    }
}
