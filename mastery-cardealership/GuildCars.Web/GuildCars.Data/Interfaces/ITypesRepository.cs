using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GuildCars.Models.Tables;
using Type = GuildCars.Models.Tables.Type;

namespace GuildCars.Data.Interfaces
{
    public interface ITypesRepository
    {
        List<Type> GetAll();
    }
}
