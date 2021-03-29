using Bogus;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class RolesRepositoryMock : IRolesRepository
    {
        private static int id = 1;
        public static Faker<Role> faker = new Faker<Role>().RuleFor(c => c.id, f => id++).RuleFor(c => c.Name, f => f.Commerce.ProductAdjective());

        List<Role> output = faker.Generate(5);

        public List<Role> GetAll()
        {
            return output;
        }
    }
}
