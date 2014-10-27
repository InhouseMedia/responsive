using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Cms.Startup))]
namespace Cms
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
