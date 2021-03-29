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
    public class ModelsRepositoryDapper : IModelsRepository
    {
        private readonly string _connectionstring;
        public ModelsRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void AddModel(Model model)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("MakeID", model.MakeID);
                param.Add("UserID", model.ID);
                param.Add("ModelName", model.ModelName);

                cn.Query<Model>("ModelInsert", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Model> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Model>("ModelsSelectAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }


        public List<Model> GetModels(int makeID)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("MakeID", makeID);

                var output = cn.Query<Model>("ModelsSelectByMake",param, commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }

    }
}
