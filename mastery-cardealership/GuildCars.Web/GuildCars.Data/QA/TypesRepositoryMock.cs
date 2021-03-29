using Bogus;
using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class TypesRepositoryMock : ITypesRepository
    {

        private static List<Models.Tables.Type> types = new List<Models.Tables.Type> { new Models.Tables.Type() { TypeID = 1, TypeName = "New" }, new Models.Tables.Type() { TypeID = 2, TypeName = "Used" } };

        public List<Models.Tables.Type> GetAll()
        {
            return types;
        }
    }
}
