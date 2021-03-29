using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class BodyStylesRepositoryMock : IBodyStylesRepository
    {
        private static List<BodyStyle> bodystyles = new List<BodyStyle> { new BodyStyle() { StyleID = 1, BodyStyleName = "Car" }, new BodyStyle() { StyleID = 2, BodyStyleName = "Truck" } };
        public List<BodyStyle> GetAll()
        {
            return bodystyles;
        }
    }
}
