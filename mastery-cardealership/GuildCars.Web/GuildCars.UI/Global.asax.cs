using Autofac;
using Autofac.Integration.Mvc;
using Autofac.Integration.WebApi;
using FluentValidation.Mvc;
using GuildCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace GuildCars.UI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            GlobalConfiguration.Configure(WebApiConfig.Register);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            FluentValidationModelValidatorProvider.Configure();

            ContainerBuilder builder = new ContainerBuilder();

            builder.RegisterApiControllers(typeof(MvcApplication).Assembly).InstancePerRequest();
            builder.RegisterControllers(typeof(MvcApplication).Assembly).InstancePerRequest();


            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    builder.RegisterAssemblyTypes(Assembly.Load("GuildCars.Data"))
                        .Where(t => t.Name.Contains("Dapper"))
                        .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository")))
                        .WithParameter(new TypedParameter(typeof(string), Settings.GetConnectionString()));
                    break;
                case "QA":
                    builder.RegisterAssemblyTypes(Assembly.Load("GuildCars.Data"))
                        .Where(t => t.Name.EndsWith("Mock"))
                        .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository"))).SingleInstance();
                    builder.RegisterType<MockUsers>().AsSelf().SingleInstance(); 
                    break;
                default:
                    throw new Exception("Could not find valid repository type configuration value");
            }

            IContainer container = builder.Build();

            GlobalConfiguration.Configuration.DependencyResolver = new AutofacWebApiDependencyResolver(container);
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            
        }
    }
}
