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
    public class TransmissionsRepositoryMock : ITransmissionsRepository
    {
        private static int id = 1;

        public static Faker<Transmission> faker = new Faker<Transmission>().RuleFor(c => c.TransmissionID, f => id++).RuleFor(c => c.TransmissionName, f => f.Commerce.ProductAdjective());

        List<Transmission> output = faker.Generate(2);

        public List<Transmission> GetAll()
        {
            return output;
        }
    }
}
