using System.Web.Http;

namespace IsTableBusy.App.Api
{
    public class WebApiApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AutoFacConfig.Register();
            FormatterConfig.RegisterFormatters(GlobalConfiguration.Configuration.Formatters);
        }
    }
}
