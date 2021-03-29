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
    public class MakeRepositoryMock : IMakesRepository
    {
        static int id = 1;
        public static Faker<Make> makes = new Faker<Make>().RuleFor(m => m.MakeID, f => id++).RuleFor(m => m.MakeName, f => f.Vehicle.Manufacturer()).RuleFor(m => m.ID, "00000000-0000-0000-0000-000000000000").RuleFor(m => m.Email, f => f.Internet.Email());



        List <Make> output = makes.Generate(5);
        public void AddMake(Make make)
        {
            make.ID = "00000000-0000-0000-0000-000000000000";
            make.CreatedDate = DateTime.Now;
            make.Email = "test@test.com";
            output.Add(make);
        }

        public List<Make> GetAll()
        {
            return output;
        }
    }
}
