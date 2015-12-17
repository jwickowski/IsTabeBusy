using Autofac;
using Autofac.Integration.WebApi;
using IsTableBusy.App.Api.Controllers;
using IsTableBusy.Core;
using IsTableBusy.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;

namespace IsTableBusy.App.Api.App_Start
{
    public static class AutoFacConfig
    {
        public static void Register()
        {
            var builder = new ContainerBuilder();

            // Get your HttpConfiguration.
            var config = GlobalConfiguration.Configuration;

            // Register your Web API controllers.
            builder.RegisterApiControllers(Assembly.GetExecutingAssembly());
            

            // OPTIONAL: Register the Autofac filter provider.
            
            builder.RegisterWebApiFilterProvider(config);

             RegisterClasses(builder);


            // Set the dependency resolver to be Autofac.
            var container = builder.Build();

            var resolver = new AutofacWebApiDependencyResolver(container);
            config.DependencyResolver = resolver;
        }

        private static void RegisterClasses(ContainerBuilder builder)
        {
            var dataAccess = Assembly.GetExecutingAssembly();



            builder.RegisterAssemblyTypes(dataAccess)
                .AsSelf()
                .AsImplementedInterfaces();



            builder
                   .RegisterType<Context>()
                   .AsSelf()
                   .InstancePerRequest();

            builder
                  .RegisterType<TableInPlaceReader>()
                  .AsSelf();

        }
    }
}