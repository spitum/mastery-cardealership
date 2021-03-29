using Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
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
    public class PurchasesRepositoryDapper : IPurchasesRepository
    {
        private readonly string _connectionstring;
        public PurchasesRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void AddPurchase(Purchase purchase)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("VehicleID", purchase.VehicleID);
                param.Add("UserID", purchase.UserID);
                param.Add("Name", purchase.Name);
                param.Add("EmailAddress", purchase.EmailAddress);
                param.Add("PhoneNumber", purchase.PhoneNumber);
                param.Add("StreetAddress1", purchase.StreetAddress1);
                param.Add("StreetAddress2", purchase.StreetAddress2);
                param.Add("City", purchase.City);
                param.Add("StateID", purchase.StateID);
                param.Add("ZipCode", purchase.ZipCode);
                param.Add("PurchasePrice", purchase.PurchasePrice);
                param.Add("PurchaseTypeID", purchase.PurchaseTypeID);


                cn.Query<Purchase>("dbo.PurchaseInsert", param, commandType: CommandType.StoredProcedure);
            }
        }

        public List<SalesReport> GetSales(SalesReportSearchParameters parameters)
        {
            List<SalesReport> sales = new List<SalesReport>();

            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("UserName", parameters.UserName);
                param.Add("FromDate", parameters.FromDate);
                param.Add("ToDate", parameters.ToDate);

                sales =  cn.Query<SalesReport>("dbo.SalesReport", param, commandType: CommandType.StoredProcedure).ToList();

                return sales;
            }

        }
    }
}
