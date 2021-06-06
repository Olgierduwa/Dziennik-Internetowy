using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ProjektPiotrMVC.Startup))]
namespace ProjektPiotrMVC
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
