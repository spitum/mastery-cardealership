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
    public class ColorsRepositoryDapper : IColorsRepository
    {
        private readonly string _connectionstring;
        public ColorsRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<Color> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Color>("SelectColorsAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }
    }
}
