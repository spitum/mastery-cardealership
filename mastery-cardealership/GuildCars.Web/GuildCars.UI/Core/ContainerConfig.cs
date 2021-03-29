using Autofac;
using GuildCars.Data;
using GuildCars.Data.Dapper;
using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GuildCars.UI.Core
{
    public static class ContainerConfig 
    {
        public static IContainer Configure()
        {
            var builder = new ContainerBuilder();

            switch (Settings.GetRepositoryType())
            {
                case "Prod":
                    builder.RegisterAssemblyTypes(Assembly.Load("GuildCars.Data"))
                        .Where(t => t.Name.Contains("Dapper"))
                        .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository")))
                        .WithParameter(new TypedParameter(typeof(string), Settings.GetConnectionString())).InstancePerDependency();
                    return builder.Build();
                case "QA":
                    builder.RegisterAssemblyTypes(Assembly.Load("GuildCars.Data"))
                        .Where(t => t.Name.EndsWith("Mock"))
                        .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository")));
                    builder.RegisterType<MockUsers>().AsSelf();
                    return builder.Build();
                default:
                    throw new Exception("Could not find valid repository type configuration value");
            }
        }
    }
}