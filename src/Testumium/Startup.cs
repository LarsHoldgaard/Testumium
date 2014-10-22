using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Testumium.Startup))]
namespace Testumium
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
