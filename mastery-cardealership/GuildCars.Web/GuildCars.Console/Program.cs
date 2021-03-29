using GuildCars.UI.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GuildCars.Data.Interfaces;

namespace GuildCars.Console
{
    class Program
    {
        static void Main(string[] args)
        {

            var container = ContainerConfig.Configure();

            using (var scope = container.BeginLifetimeScope())
            {
                var app = scope.Resolve<IVehiclesRepository>();

                var makes = scope.Resolve<IMakesRepository>();

                foreach (var car in app.GetFeaturedVehicles())
                {
                    System.Console.WriteLine($"{car.MakeName}: {car.ModelName}");
                }

                foreach (var car in app.GetInventory(2))
                {
                    System.Console.WriteLine($"{car.MakeName}: {car.ModelName}");
                }

                foreach (var m in makes.GetAll())
                {
                    System.Console.WriteLine($"{m.MakeName}");
                }

                System.Console.ReadLine();
            }


        }
    }
}
