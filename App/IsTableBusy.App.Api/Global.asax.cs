using Autofac;
using Autofac.Integration.WebApi;
using IsTableBusy.App.Api.App_Start;
using System.Web.Http;
using System.Web.Routing;

namespace IsTableBusy.App.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoFacConfig.Register();    
        }
    }
}
