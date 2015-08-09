using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(ReviewsJoy.Startup))]
namespace ReviewsJoy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
