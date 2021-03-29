using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Data.Interfaces
{
    public interface IVehiclesRepository
    {
        List<FeaturedVehicles> GetFeaturedVehicles();

        List<InventoryReport> GetInventory(int typeID);

        List<VehicleShortItem> SelectInventory(InventorySearchParameters parameters);

        VehicleItem SelectVehicleDetails(int vehicleID);

        Vehicle SelectVehicleByID(int vehicleID);

        void AddVehicle(Vehicle vehicle);

        void UpdateVehicle(Vehicle vehicle);

        void DeleteVehicle(int vehicleID);
    }
}
