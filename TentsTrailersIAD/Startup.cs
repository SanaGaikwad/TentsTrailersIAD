using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(TentsTrailersIAD.Startup))]
namespace TentsTrailersIAD
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
