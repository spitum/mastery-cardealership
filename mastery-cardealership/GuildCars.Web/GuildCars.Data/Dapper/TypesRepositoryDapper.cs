using Dapper;
using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Dapper
{
    public class TypesRepositoryDapper : ITypesRepository
    {
        private readonly string _connectionstring;
        public TypesRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<Models.Tables.Type> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Models.Tables.Type>("SelectTypeAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }

    }
}
