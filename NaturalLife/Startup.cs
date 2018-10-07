using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(NaturalLife.Startup))]
namespace NaturalLife
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
