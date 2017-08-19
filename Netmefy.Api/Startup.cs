using Microsoft.Owin;
using Owin;

[assembly: OwinStartup(typeof(Netmefy.Startup))]

namespace Netmefy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureOAuth(app);
        }
    }
}
