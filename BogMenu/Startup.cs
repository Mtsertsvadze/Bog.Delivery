using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(BogMenu.Startup))]
namespace BogMenu
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
