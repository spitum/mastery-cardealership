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
    public class ModelRepositoryMock : IModelsRepository
    {
        private static int id = 1;
        static List<Make> makes = new MakeRepositoryMock().GetAll();
        public static Faker<Model> models = new Faker<Model>().RuleFor(m => m.MakeID, f => f.PickRandom(11,15))
                                                              .RuleFor(m => m.ModelID, f => id++)
                                                              .RuleFor(m => m.ModelName, f => f.Vehicle.Model())
                                                              .RuleFor(m => m.ID, f => "00000000-0000-0000-0000-000000000000")
                                                              .RuleFor(m => m.Email, f => f.Internet.Email());

        List<Model> output = models.Generate(25);


        public void AddModel(Model model)
        {
            model.CreatedDate = DateTime.Now;
            model.ID = "00000000-0000-0000-0000-000000000000";
            model.Email = "test@test.com";
            output.Add(model);
        }

        public List<Model> GetAll()
        {
            return output;
        }

        public List<Model> GetModels(int makeID)
        {
            return output.Where(m => m.MakeID == makeID).ToList();
        }
    }
}
