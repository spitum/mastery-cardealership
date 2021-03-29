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
    public class PurchaseTypesRepositoryMock : IPurchaseTypesRepository
    {
        private static int id = 1;
        public static Faker<PurchaseType> purchasetypes = new Faker<PurchaseType>().RuleFor(c => c.PurchaseTypeID, f => id++).RuleFor(c => c.PurchaseTypeName, f => f.Commerce.ProductAdjective());
       
        List<PurchaseType> types = purchasetypes.Generate(5);

        public List<PurchaseType> GetAll()
        {
            return types;
        }
    }
}
