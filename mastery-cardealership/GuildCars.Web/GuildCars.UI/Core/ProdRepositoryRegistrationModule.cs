using Autofac;
using GuildCars.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace GuildCars.UI.Core
{
    public class ProdRepositoryRegistrationModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {

                    builder.RegisterAssemblyTypes(Assembly.Load("GuildCars.Data"))
                        .Where(t => t.Name.Contains("Dapper"))
                        .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name.EndsWith("Repository")))
                        .WithParameter("connectionstring", Settings.GetConnectionString());
            
        }
    }
}