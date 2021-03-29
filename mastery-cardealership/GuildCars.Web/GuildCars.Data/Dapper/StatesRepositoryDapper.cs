using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Dapper
{
    public class StatesRepositoryDapper : IStatesRepository
{
        string _connectionstring;

        public StatesRepositoryDapper(string connectionString)
        {
            _connectionstring = connectionString;
        }

        public List<State> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<State>("StatesSelectAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }  
        }


    }
}
