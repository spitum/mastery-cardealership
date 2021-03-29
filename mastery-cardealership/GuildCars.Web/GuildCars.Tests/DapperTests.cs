using GuildCars.Data;
using GuildCars.Data.Dapper;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Tests
{
    [TestFixture]
    public class DapperTests
    {
        private string cn = Settings.GetConnectionString();

        [SetUp]
        public void Init()
        {
            using (var cxn = new SqlConnection(cn))
            {
                var cmd = new SqlCommand();
                cmd.CommandText = "DbReset";
                cmd.CommandType = System.Data.CommandType.StoredProcedure;

                cmd.Connection = cxn;

                cxn.Open();

                cmd.ExecuteNonQuery();
            }

        }

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepositoryDapper(cn);

            var states = repo.GetAll();

            Assert.AreEqual(18, states.Count);

            Assert.AreEqual("AR", states[0].StateID);
            Assert.AreEqual("Arkansas", states[0].StateName);
        }

        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStylesRepositoryDapper(cn);

            var styles = repo.GetAll();

            Assert.AreEqual(3, styles.Count);

            Assert.AreEqual(1, styles[0].StyleID);
            Assert.AreEqual("Car", styles[0].BodyStyleName);
        }

        [Test]
        public void CanLoadColors()
        {
            var repo = new ColorsRepositoryDapper(cn);

            var colors = repo.GetAll();

            Assert.AreEqual(8, colors.Count);

            Assert.AreEqual(1, colors[0].ColorID);
            Assert.AreEqual("Blue", colors[0].ColorName);
        }

        [Test]
        public void CanLoadInteriorColors()
        {
            var repo = new InteriorColorsRepositoryDapper(cn);

            var colors = repo.GetAll();

            Assert.AreEqual(3, colors.Count);

            Assert.AreEqual(1, colors[0].InteriorColorID);
            Assert.AreEqual("Black", colors[0].InteriorColorName);
        }

        [Test]
        public void CanLoadMakes()
        {
            var repo = new MakesRepositoryDapper(cn);

            var makes = repo.GetAll();

            Assert.AreEqual(10, makes.Count);

            Assert.AreEqual(1, makes[0].MakeID);
            Assert.AreEqual("Acura", makes[0].MakeName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", makes[0].ID);
            Assert.AreEqual("test@test.com", makes[0].Email);
        }

        [Test]
        public void CanLoadModels()
        {
            var repo = new ModelsRepositoryDapper(cn);

            var models = repo.GetAll();

            Assert.AreEqual(13, models.Count);

            Assert.AreEqual(1, models[0].ModelID);
            Assert.AreEqual(1, models[0].MakeID);
            Assert.AreEqual("NSX", models[0].ModelName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", models[0].ID);
            Assert.AreEqual("test@test.com", models[0].Email);
        }

        [Test]
        public void CanLoadModelsByMake()
        {
            var repo = new ModelsRepositoryDapper(cn);

            var models = repo.GetModels(1);

            Assert.AreEqual(4, models.Count);

            Assert.AreEqual(1, models[0].ModelID);
            Assert.AreEqual(1, models[0].MakeID);
            Assert.AreEqual("NSX", models[0].ModelName);
            Assert.AreEqual("00000000-0000-0000-0000-000000000000", models[0].ID);
            Assert.AreEqual("test@test.com", models[0].Email);
        }


        [Test]
        public void CanAddMake()
        {
            Make make = new Make();

            var repo = new MakesRepositoryDapper(cn);

            make.MakeName = "Honda";
            make.ID = "00000000-0000-0000-0000-000000000000";

            repo.AddMake(make);

            repo.GetAll();

            Assert.AreEqual(11, repo.GetAll().Count());

        }

        [Test]
        public void CanAddModel()
        {
            Model model = new Model();

            var repo = new ModelsRepositoryDapper(cn);

            model.MakeID = 10;
            model.ID = "00000000-0000-0000-0000-000000000000";
            model.ModelName = "4Runner";

            repo.AddModel(model);

            repo = new ModelsRepositoryDapper(cn);

            Assert.AreEqual(14, repo.GetAll().Count());

            //Assert.AreEqual(14, repo.GetAll().Where(m => m.ModelName == "4Runner").Select(m => m.ModelID));

        }

        [Test]
        public void CanLoadPurchaseTypes()
        {
            var repo = new PurchaseTypesRepositoryDapper(cn);

            var puchaseTypes = repo.GetAll();

            Assert.AreEqual(3, puchaseTypes.Count);

            Assert.AreEqual(1, puchaseTypes[0].PurchaseTypeID);
            Assert.AreEqual("Bank Finance", puchaseTypes[0].PurchaseTypeName);

        }

        [Test]
        public void CanLoadTypes()
        {
            var repo = new TypesRepositoryDapper(cn);

            var Types = repo.GetAll();

            Assert.AreEqual(2, Types.Count);

            Assert.AreEqual(1, Types[0].TypeID);
            Assert.AreEqual("New", Types[0].TypeName);

        }

        [Test]
        public void CanLoadRoles()
        {
            var repo = new RolesRepositoryDapper(cn);

            var roles = repo.GetAll();

            Assert.AreEqual(3, roles.Count);

            Assert.AreEqual(1, roles[0].id);
            Assert.AreEqual("Admin", roles[0].Name);

        }

        [Test]
        public void CanLoadTranmissions()
        {
            var repo = new TransmissionsRepositoryDapper(cn);

            var roles = repo.GetAll();

            Assert.AreEqual(2, roles.Count);

            Assert.AreEqual(1, roles[0].TransmissionID);
            Assert.AreEqual("Automatic", roles[0].TransmissionName);

        }

        [Test]
        public void CanLoadSpecials()
        {
            var repo = new SpecialsRepositoryDapper(cn);

            var roles = repo.GetAll();

            Assert.AreEqual(5, roles.Count);

            Assert.AreEqual(1, roles[0].SpecialID);
            Assert.AreEqual("Summer Deal", roles[0].SpecialTitle);
            Assert.AreEqual("Object-based encompassing secured line", roles[0].SpecialDescription);

        }

        [Test]
        public void CanAddSpecial()
        {
            Special model = new Special();

            var repo = new SpecialsRepositoryDapper(cn);

            model.SpecialTitle = "Test";
            model.SpecialDescription = "Test Special";

            repo.AddSpecial(model);

            repo = new SpecialsRepositoryDapper(cn);

            Assert.AreEqual(6, repo.GetAll().Count());

            //Assert.AreEqual(14, repo.GetAll().Where(m => m.ModelName == "4Runner").Select(m => m.ModelID));

        }

        [Test]
        public void CanDeleteSpecial()
        {
            var repo = new SpecialsRepositoryDapper(cn);

            repo.RemoveSpecial(5);

            Assert.AreEqual(4, repo.GetAll().Count());
        }

        [Test]
        public void CanAddContact()
        {
            Contact model = new Contact();

            var repo = new ContactsRepositoryDapper(cn);

            model.ContactName = "Test";
            model.EmailAddress = "test@testoncta.com";
            model.PhoneNumber = "505-555-555";
            model.Message = "Test Contact";

            repo.AddContact(model);

            repo = new ContactsRepositoryDapper(cn);

            Assert.AreEqual(1, repo.GetAll().Count());


        }

        [Test]
        public void CanLoadSalesReport()
        {
            SalesReport model = new SalesReport();

            var repo = new PurchasesRepositoryDapper(cn);


            var sales = repo.GetSales(new SalesReportSearchParameters {FromDate = null,ToDate = null, UserName = "test" }).ToList();

            Assert.AreEqual(1, sales.Count());

            Assert.AreEqual(3, sales[0].TotalVehicles);


        }

        [Test]
        public void CanAddPurchase()
        {
            Purchase model = new Purchase();

            var repo = new PurchasesRepositoryDapper(cn);

            model.VehicleID = 4;
            model.UserID = "00000000-0000-0000-0000-000000000000";
            model.Name = "Test Buyer";
            model.EmailAddress = "testbuyer@test2.com";
            model.PhoneNumber = "555-555-5555";
            model.StreetAddress1 = "1 Bayside Crossing";
            model.City = "Lost City";
            model.StateID = "NM";
            model.ZipCode = 87109;
            model.PurchasePrice = 39324.20M;
            model.PurchaseTypeID = 1;


            repo.AddPurchase(model);

            repo = new PurchasesRepositoryDapper(cn);

            var sales = repo.GetSales(new SalesReportSearchParameters { FromDate = null, ToDate = null, UserName = "test" }).ToList();

            Assert.AreEqual(4, sales[0].TotalVehicles);

            Assert.AreEqual(142357.08M, sales[0].TotalSales);

        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {

            var repo = new VehiclesRepositoryDapper(cn);

            var featuredVehicles = repo.GetFeaturedVehicles().ToList();

            Assert.AreEqual(5, featuredVehicles.Count());

            Assert.AreEqual(6, featuredVehicles[0].VehicleID);
        }

        [Test]
        public void CanLoadInventoryReport()
        {
            InventoryReport model = new InventoryReport();

            var repo = new VehiclesRepositoryDapper(cn);


            var inventoryReports = repo.GetInventory(1).ToList();

            Assert.AreEqual(4, inventoryReports.Count());

            Assert.AreEqual(2, inventoryReports[3].Count);


        }

        [Test]
        public void CanVehicleDetails()
        {

            var repo = new VehiclesRepositoryDapper(cn);


            var vehicle = repo.SelectVehicleDetails(5);

            Assert.AreEqual(5, vehicle.VehicleID);

            Assert.AreEqual("JM1NC2SF1F0384664", vehicle.VINNumber);


        }

        [Test]
        public void CanSelectInventoryNew()
        {
            

            var repo = new VehiclesRepositoryDapper(cn);


            var vehicles = repo.SelectInventory(new InventorySearchParameters {QuickSearch=null,TypeName="new",MinPrice=null,MaxPrice=null,MinYear=null,MaxYear=null}).ToList();


            Assert.AreEqual(4, vehicles.Count());

            vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null,  TypeName = "new", MinPrice = null, MaxPrice = null, MinYear = 2020, MaxYear = 2020 }).ToList();

            Assert.AreEqual(2, vehicles.Count());

            vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null,  TypeName = "new", MinPrice = null, MaxPrice = 18000M, MinYear = null, MaxYear = 2021 }).ToList();

            Assert.AreEqual(1, vehicles.Count());

        }

        [Test]
        public void CanSelectInventoryUsed()
        {


            var repo = new VehiclesRepositoryDapper(cn);


            var vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null, TypeName = "used", MinPrice = null, MaxPrice = null, MinYear = null, MaxYear = null }).ToList();


            Assert.AreEqual(3, vehicles.Count());

            vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null, TypeName = "used", MinPrice = null, MaxPrice = null, MinYear = 2020, MaxYear = 2020 }).ToList();

            Assert.AreEqual(1, vehicles.Count());

            vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null, TypeName = "used", MinPrice = null, MaxPrice = 18000M, MinYear = null, MaxYear = 2018 }).ToList();

            Assert.IsEmpty(vehicles);

        }

        [Test]
        public void CanAddVehicle()
        {
            Vehicle model = new Vehicle();

            var repo = new VehiclesRepositoryDapper(cn);

            model.MakeID = 3;
            model.ModelID = 2;
            model.TypeID = 2;
            model.StyleID = 2;
            model.Year = 2015;
            model.TransmissionID = 1;
            model.ColorID = 3;
            model.InteriorColorID = 1;
            model.Mileage = 11101;
            model.VINNumber = "WAUCFAFH7BN075395";
            model.MSRP = 13999;
            model.SalePrice = 12999;
            model.Description = "Test Insert";
            model.ImageFileName = "Tesla.png";


            repo.AddVehicle(model);

            repo = new VehiclesRepositoryDapper(cn);

            var vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null, TypeName = "used", MinPrice = null, MaxPrice = null, MinYear = null, MaxYear = null }).ToList();

            Assert.AreEqual(4, vehicles.Count());

            Assert.AreEqual(11, model.VehicleID);

        }

        [Test]
        public void CanUpdateVehicle()
        {

            var repo = new VehiclesRepositoryDapper(cn);

            var model = repo.SelectVehicleByID(2);

            model.MakeID = 3;
            model.ModelID = 2;
            model.TypeID = 1;
            model.StyleID = 1;
            model.Year = 2015;
            model.TransmissionID = 1;
            model.ColorID = 3;
            model.InteriorColorID = 1;
            model.Mileage = 11101;
            model.VINNumber = "WAUCFAFH7BN075395";
            model.MSRP = 13999;
            model.SalePrice = 10999;
            model.Description = "Test Insert";
            model.ImageFileName = "Tesla.png";
            model.Featured = true;


            repo.UpdateVehicle(model);

            repo = new VehiclesRepositoryDapper(cn);

             var updatedmodel = repo.SelectVehicleDetails(2);
            //var vehicles = repo.SelectInventory(new InventorySearchParameters { MakeName = null, ModelName = null, Year = null, TypeName = "used", MinPrice = null, MaxPrice = null, MinYear = null, MaxYear = null }).ToList();

            Assert.AreEqual(1, updatedmodel.TypeID);

            Assert.AreEqual(10999, updatedmodel.SalePrice);

        }

    }
}
