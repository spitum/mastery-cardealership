using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace GuildCars.Data.QA
{
    public class InteriorColorsRepositoryMock : IInteriorColorsRepository
    {
        private static int id = 1;
        public static Faker<InteriorColor> colors = new Faker<InteriorColor>().RuleFor(c => c.InteriorColorID, f => id++).RuleFor(c => c.InteriorColorName, f => f.Commerce.Color());
        public List<InteriorColor> GetAll()
        {
            var output = colors.Generate(5).ToList();
            return output;
        }
    }
}
