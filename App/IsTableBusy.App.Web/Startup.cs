using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(IsTableBusy.App.Web.Startup))]
namespace IsTableBusy.App.Web
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
        }
    }
}
