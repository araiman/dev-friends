using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(DevFriends.Startup))]
namespace DevFriends
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
