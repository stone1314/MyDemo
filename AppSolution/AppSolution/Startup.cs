using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(AppSolution.Startup))]
namespace AppSolution
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
