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
    public class TransmissionsRepositoryDapper : ITransmissionsRepository
    {
        private readonly string _connectionstring;
        public TransmissionsRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<Transmission> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Transmission>("SelectTransmissionsAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }
    }
}
