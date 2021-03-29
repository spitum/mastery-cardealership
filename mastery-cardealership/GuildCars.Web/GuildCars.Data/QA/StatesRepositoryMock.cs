using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.QA
{
    public class StatesRepositoryMock : IStatesRepository
    {
        private static List<State> states = new List<State> { new State() { StateID = "NM", StateName = "New Mexico" }, new State() { StateID = "TX", StateName = "Texas" } }; 
        public List<State> GetAll()
        {
            return states;
        }
    }
}
