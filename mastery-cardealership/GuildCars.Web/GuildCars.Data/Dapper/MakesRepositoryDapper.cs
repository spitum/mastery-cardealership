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
    public class MakesRepositoryDapper : IMakesRepository
    {
        private readonly string _connectionstring;
        public MakesRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void AddMake(Make make)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("MakeName", make.MakeName);
                param.Add("UserID", make.ID);

                cn.Query<Make>("dbo.MakeInsert", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Make> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Make>("MakesSelectAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }
    }
}
