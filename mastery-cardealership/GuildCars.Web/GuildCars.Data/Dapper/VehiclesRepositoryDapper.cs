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
    public class VehiclesRepositoryDapper : IVehiclesRepository
    {
        private readonly string _connectionstring;
        public VehiclesRepositoryDapper(string connectionstring)
        {
            _connectionstring = connectionstring;
        }
        public void AddVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("MakeID", vehicle.MakeID);
                param.Add("ModelID", vehicle.ModelID);
                param.Add("TypeID", vehicle.TypeID);
                param.Add("StyleID", vehicle.StyleID);
                param.Add("Year", vehicle.Year);
                param.Add("TransmissionID", vehicle.TransmissionID);
                param.Add("ColorID", vehicle.ColorID);
                param.Add("InteriorColorID", vehicle.InteriorColorID);
                param.Add("Mileage", vehicle.Mileage);
                param.Add("VINNumber", vehicle.VINNumber);
                param.Add("MSRP", vehicle.MSRP);
                param.Add("SalePrice", vehicle.SalePrice);
                param.Add("Description", vehicle.Description);
                param.Add("ImageFileName", vehicle.ImageFileName);
                param.Add("VehicleID", dbType: DbType.Int32, direction: ParameterDirection.Output);

                cn.Query<Special>("dbo.VehicleInsert", param, commandType: CommandType.StoredProcedure);
                vehicle.VehicleID = param.Get<int>("VehicleID");
            }
        }
    

        public List<FeaturedVehicles> GetFeaturedVehicles()
        {

            using (var cn = new SqlConnection(_connectionstring))
            {

                var output = cn.Query<FeaturedVehicles>("FeaturedSelectAll", commandType: CommandType.StoredProcedure).ToList();
                return output;
            }
        }

        public List<InventoryReport> GetInventory(int typeID)
        {
            List<InventoryReport> inventory = new List<InventoryReport>();

            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("TypeID", typeID);

                inventory = cn.Query<InventoryReport>("dbo.InventoryReport", param, commandType: CommandType.StoredProcedure).ToList();

                return inventory;
            }
        }


        public List<VehicleShortItem> SelectInventory(InventorySearchParameters parameters)
        {
            List<VehicleShortItem> vehicles = new List<VehicleShortItem>();

            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("QuickSearch", parameters.QuickSearch);
                param.Add("Type", parameters.TypeName);
                param.Add("MinPrice", parameters.MinPrice);
                param.Add("MaxPrice", parameters.MaxPrice);
                param.Add("MinYear", parameters.MinYear);
                param.Add("MaxYear", parameters.MaxYear);

                vehicles = cn.Query<VehicleShortItem>("dbo.SelectInventory", param, commandType: CommandType.StoredProcedure).ToList();

                return vehicles;
            }

        }

        public Vehicle SelectVehicleByID(int vehicleID)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("VehicleID", vehicleID);

                var output = cn.Query<Vehicle>("dbo.SelectVehicleByID", param, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return output;
            }
        }

        public VehicleItem SelectVehicleDetails(int vehicleID)
        {

            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("VehicleID", vehicleID);

                var output = cn.Query<VehicleItem>("dbo.SelectVehicleDetails", param, commandType: CommandType.StoredProcedure).FirstOrDefault();

                return output;
            }
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            using (var cn = new SqlConnection(_connectionstring))
            {
                var param = new DynamicParameters();
                param.Add("MakeID", vehicle.MakeID);
                param.Add("ModelID", vehicle.ModelID);
                param.Add("TypeID", vehicle.TypeID);
                param.Add("StyleID", vehicle.StyleID);
                param.Add("Year", vehicle.Year);
                param.Add("TransmissionID", vehicle.TransmissionID);
                param.Add("ColorID", vehicle.ColorID);
                param.Add("InteriorColorID", vehicle.InteriorColorID);
                param.Add("Mileage", vehicle.Mileage);
                param.Add("VINNumber", vehicle.VINNumber);
                param.Add("MSRP", vehicle.MSRP);
                param.Add("SalePrice", vehicle.SalePrice);
                param.Add("Description", vehicle.Description);
                param.Add("ImageFileName", vehicle.ImageFileName);
                param.Add("VehicleID", vehicle.VehicleID);
                param.Add("Featured", vehicle.Featured);

                cn.Query<Special>("dbo.VehicleUpdate", param, commandType: CommandType.StoredProcedure);
            }
        }

        public void DeleteVehicle(int vehicleID)
        {
            using (IDbConnection connection = new System.Data.SqlClient.SqlConnection(Settings.GetConnectionString()))
            {
                connection.Execute("VehicleDelete @VehicleID", new { VehicleID = vehicleID });
            }
        }
    }


}

