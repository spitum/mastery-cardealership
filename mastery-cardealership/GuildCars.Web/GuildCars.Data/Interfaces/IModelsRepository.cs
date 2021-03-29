using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IModelsRepository
    {
        List<Model> GetAll();

        List<Model> GetModels(int makeID);

        void AddModel(Model model);
    }
}
