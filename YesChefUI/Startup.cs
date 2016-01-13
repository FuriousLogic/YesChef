using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(YesChefUI.Startup))]
namespace YesChefUI
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
