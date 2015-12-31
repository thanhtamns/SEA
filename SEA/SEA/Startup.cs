using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SEA.Startup))]
namespace SEA
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
