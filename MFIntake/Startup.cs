using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(MFIntake.Startup))]
namespace MFIntake
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
