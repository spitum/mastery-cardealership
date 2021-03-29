using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;
using Bogus.Extensions;

namespace GuildCars.Data.QA
{
    public class VehiclesRepositoryMock : IVehiclesRepository
    {
        private static int id = 1;
        public  static Faker<Vehicle> vehicleTable = new Faker<Vehicle>().RuleFor(c => c.VehicleID, f => id++)
                                                            .RuleFor(c => c.ModelID, f => f.PickRandom(1,models.Max(m => m.ModelID)))
                                                            .RuleFor(c => c.MakeID, f => f.PickRandom(1, makes.Max(m => m.MakeID)))
                                                            .RuleFor(c => c.TypeID, f => f.PickRandom(1, types.Max(m => m.TypeID)))
                                                            .RuleFor(c => c.StyleID, f => f.PickRandom(1, styles.Max(m => m.StyleID)))
                                                            .RuleFor(c => c.TransmissionID, f => f.PickRandom(1, transmissions.Max(m => m.TransmissionID)))
                                                            .RuleFor(c => c.ColorID, f => f.PickRandom(1, colors.Max(m => m.ColorID)))
                                                            .RuleFor(c => c.InteriorColorID, f => f.PickRandom(1, interiorcolors.Max(m => m.InteriorColorID)))
                                                            .RuleFor(c => c.Year, f => f.Random.Int(2010, 2021))
                                                            .RuleFor(c => c.Mileage, f => f.Random.Int(10500, 99999))
                                                            .RuleFor(c => c.MSRP, f => f.Finance.Random.Decimal(1000M, 100000M))
                                                            .RuleFor(c => c.SalePrice, f => f.Finance.Random.Decimal(1000M, 100000M).OrNull(f,0.1f))
                                                            .RuleFor(c => c.Description, f => f.Lorem.Sentence(3))
                                                            .RuleFor(c => c.Featured, f => f.Random.Bool())
                                                            .RuleFor(c => c.VINNumber, f => f.Lorem.Word())
                                                            .RuleFor(c => c.ImageFileName, "inventory-11.jpg");

        static List<Make> makes  = new MakeRepositoryMock().GetAll();
        static List<Model> models = new ModelRepositoryMock().GetAll();
        static List<BodyStyle> styles = new BodyStylesRepositoryMock().GetAll();
        static List<Transmission> transmissions = new TransmissionsRepositoryMock().GetAll();
        static List<Color> colors = new ColorsRepositoryMock().GetAll();
        static List<InteriorColor> interiorcolors = new InteriorColorsRepositoryMock().GetAll();
        static List<Models.Tables.Type> types = new TypesRepositoryMock().GetAll();

        public  List<Vehicle> output { get; set; } = vehicleTable.Generate(20);

        public  List<VehicleItem> GetVehicleItems()
        {
            return (from o in output
                    join m in makes on o.MakeID equals m.MakeID
                    join mo in models on o.ModelID equals mo.ModelID
                    join s in styles on o.StyleID equals s.StyleID
                    join t in transmissions on o.TransmissionID equals t.TransmissionID
                    join c in colors on o.ColorID equals c.ColorID
                    join oc in interiorcolors on o.InteriorColorID equals oc.InteriorColorID
                    join ty in types on o.TypeID equals ty.TypeID
                    select new VehicleItem()
                    {
                        VehicleID = o.VehicleID,
                        MakeID = o.MakeID,
                        ModelID = o.ModelID,
                        ModelName = mo.ModelName,
                        MakeName = m.MakeName,
                        TypeID = o.TypeID,
                        TypeName = ty.TypeName,
                        StyleID = o.StyleID,
                        BodyStyleName = s.BodyStyleName,
                        Year = o.Year,
                        TransmissionID = o.TransmissionID,
                        TransmissionName = t.TransmissionName,
                        ColorID = o.ColorID,
                        ColorName = c.ColorName,
                        InteriorColorID = oc.InteriorColorID,
                        InteriorColorName = oc.InteriorColorName,
                        Mileage = o.Mileage,
                        VINNumber = o.VINNumber,
                        MSRP = o.MSRP,
                        SalePrice = o.SalePrice,
                        ImageFileName = o.ImageFileName,
                        Description = o.Description
                    }).ToList();
        }

        public  List<VehicleShortItem> GenerateVehicleShortItems()
        {
            return (from v in GetVehicleItems()
                    select new VehicleShortItem()
                    {
                        VehicleID = v.VehicleID,
                        ModelID = v.ModelID,
                        MakeID = v.MakeID,
                        TypeID = v.TypeID,
                        StyleID = v.StyleID,
                        TransmissionID = v.TransmissionID,
                        ColorID = v.ColorID,
                        InteriorColorID = v.InteriorColorID,
                        Mileage = v.Mileage,
                        Year = v.Year,
                        MSRP = v.MSRP,
                        SalePrice = v.SalePrice,
                        ImageFileName = v.ImageFileName,
                        MakeName = v.MakeName,
                        ModelName = v.ModelName,
                        TypeName = v.TypeName,
                        BodyStyleName = v.BodyStyleName,
                        TransmissionName = v.TransmissionName,
                        ColorName = v.ColorName,
                        InteriorColorName = v.InteriorColorName,
                        Purchased = false
                    }
                    ).ToList();
        }

        public  List<FeaturedVehicles> GenerateFeaturedVehicles()
        {
            return (from o in output
                    join m in makes on o.MakeID equals m.MakeID
                    join mo in models on o.ModelID equals mo.ModelID
                    where o.Featured == true
                    select new FeaturedVehicles()
                    {
                        VehicleID = o.VehicleID,
                        MakeID = o.MakeID,
                        ModelID = o.ModelID,
                        ModelName = mo.ModelName,
                        MakeName = m.MakeName,
                        ImageFileName = o.ImageFileName,
                        Year = o.Year,
                        SalePrice = o.SalePrice ?? 0
                    }).ToList();
        }

        public  List<InventoryReport> GenerateInventory()
        {
            var query = (from o in output
                         join m in makes on o.MakeID equals m.MakeID
                         join mo in models on o.ModelID equals mo.ModelID
                         join ty in types on o.TypeID equals ty.TypeID
                         group new { o.VehicleID, o.MSRP } by new { o.TypeID, ty.TypeName, o.MakeID, o.ModelID, m.MakeName, mo.ModelName, o.Year } into g

                         select new InventoryReport()
                         {
                             TypeID = g.Key.TypeID,
                             TypeName = g.Key.TypeName,
                             MakeID = g.Key.MakeID,
                             ModelID = g.Key.ModelID,
                             ModelName = g.Key.ModelName,
                             MakeName = g.Key.MakeName,
                             Year = g.Key.Year,
                             StockValue = g.Sum(s => s.MSRP),
                             Count = g.Count()
                         }) .ToList();

            return query;

        }



        public void AddVehicle(Vehicle vehicle)
        {
            output.Add(vehicle);
        }

        public List<FeaturedVehicles> GetFeaturedVehicles()
        {
            return GenerateFeaturedVehicles().ToList();
        }

        public List<InventoryReport> GetInventory(int typeID)
        {
            return GenerateInventory().Where(i => i.TypeID == typeID).ToList();
                         
        }

        public List<VehicleShortItem> SelectInventory(InventorySearchParameters parameters)
        {
            List<VehicleShortItem> shortList = new List<VehicleShortItem>();
            List<VehicleShortItem> output = new List<VehicleShortItem>();

            if (parameters.TypeName == null)
            {
                shortList = GenerateVehicleShortItems();
            }

            else
            {
                shortList = GenerateVehicleShortItems().Where(v => v.TypeName == parameters.TypeName).ToList();
            }


            if (parameters.QuickSearch != null)
            {

                output.AddRange(shortList.Where(v => v.MakeName.Contains(parameters.QuickSearch) || v.ModelName.Contains(parameters.QuickSearch) || v.Year.ToString().Contains(parameters.QuickSearch.ToString())));
            }

            else if(parameters.MinPrice != null)
            {
                output.AddRange(shortList.Where(v => v.SalePrice >= parameters.MinPrice));
            }

            else if(parameters.MaxPrice != null)
            {
                output.AddRange(shortList.Where(v => v.SalePrice <= parameters.MaxPrice));
            }

            else if(parameters.MinYear != null)
            {
                output.AddRange(shortList.Where(v => v.Year >= parameters.MinYear));
            }

            else if(parameters.MaxYear != null)
            {
                output.AddRange(shortList.Where(v => v.Year <= parameters.MaxYear));
            }

            else output = shortList;

            return output;

        }

        public VehicleItem SelectVehicleDetails(int vehicleID)
        {
            return GetVehicleItems().Where(v => v.VehicleID == vehicleID).FirstOrDefault();
        }

        public void UpdateVehicle(Vehicle vehicle)
        {
            var found = SelectVehicleDetails(vehicle.VehicleID);

            found.VehicleID = vehicle.VehicleID;
            found.Year = vehicle.Year;
            found.TypeID = vehicle.TypeID;
            found.TransmissionID = vehicle.TransmissionID;
            found.VINNumber = vehicle.VINNumber;
            found.Featured = vehicle.Featured;
            found.SalePrice = vehicle.SalePrice;
            found.Description = vehicle.Description;
            found.ImageFileName = vehicle.ImageFileName;
        }

        public Vehicle SelectVehicleByID(int vehicleID)
        {
            var found = output.FirstOrDefault(v => v.VehicleID == vehicleID);

            return found;
        }

        public void DeleteVehicle(int vehicleID)
        {
            var found = output.FirstOrDefault(v => v.VehicleID == vehicleID);

            output.Remove(found);
        }
    }
}
