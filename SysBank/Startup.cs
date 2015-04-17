using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(SysBank.Startup))]
namespace SysBank
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
