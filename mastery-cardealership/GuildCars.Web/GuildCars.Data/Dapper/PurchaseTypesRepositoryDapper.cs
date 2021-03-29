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
    public class PurchaseTypesRepositoryDapper : IPurchaseTypesRepository
    {
        private readonly string _connectionstring;
        public PurchaseTypesRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public List<PurchaseType> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<PurchaseType>("SelectPurchaseTypesAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }
    }
}
