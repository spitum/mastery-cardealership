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
    public class SpecialsRepositoryMock : ISpecialsRepository
    {
        private static int id = 1;

        public static Faker<Special> faker = new Faker<Special>().RuleFor(c => c.SpecialID, f => id++)
                                                            .RuleFor(c => c.SpecialTitle, f => f.Lorem.Sentence(1))
                                                            .RuleFor(c => c.SpecialDescription, f => f.Lorem.Sentence(5));
        List<Special> output = faker.Generate(5);
        public void AddSpecial(Special special)
        {
            output.Add(special);
        }

        public List<Special> GetAll()
        {
            return output;
        }

        public void RemoveSpecial(int specialID)
        {
            output.RemoveAll(s => s.SpecialID == specialID);
        }
    }
}
