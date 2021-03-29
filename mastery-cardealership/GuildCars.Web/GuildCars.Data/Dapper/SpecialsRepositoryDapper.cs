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
    public class SpecialsRepositoryDapper : ISpecialsRepository
    {

        private readonly string _connectionstring;
        public SpecialsRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void AddSpecial(Special special)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("SpecialTitle", special.SpecialTitle);
                param.Add("SpecialDescription", special.SpecialDescription);

                cn.Query<Special>("dbo.SpecialInsert", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<Special> GetAll()
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var output = cn.Query<Special>("SpecialsSelectAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }

        public void RemoveSpecial(int specialID)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                cn.Execute("SpecialDelete @SpecialID", new { SpecialID = specialID });
            }
        }
    }
}
