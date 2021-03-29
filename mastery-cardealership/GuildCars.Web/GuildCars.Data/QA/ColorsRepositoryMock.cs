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
    public class ColorsRepositoryMock : IColorsRepository
    {
        static int id = 1;
        public static Faker<Color> colors = new Faker<Color>().RuleFor(c => c.ColorID, f => id++).RuleFor(c => c.ColorName, f => f.Commerce.Color());
        public List<Color> GetAll()
        {
            var output = colors.Generate(5).ToList();
            return output;
        }
    }
}
