using GuildCars.Data.Interfaces;
using GuildCars.Data.QA;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using GuildCars.UI.Core;
using Autofac.Core;
using Autofac.Core.Registration;

namespace GuildCars.Tests
{
    [TestFixture]
    public class MockTests
    {

        [Test]
        public void CanLoadStates()
        {
            var repo = new StatesRepositoryMock();

            Assert.AreEqual(2, repo.GetAll().Count());
        }

        [Test]
        public void CanLoadBodyStyles()
        {
            var repo = new BodyStylesRepositoryMock();

            Assert.AreEqual(2, repo.GetAll().Count());
        }

        [Test]
        public void CanLoadContact()
        {
            var repo = new ContactsRepositoryMock();

            Assert.AreEqual(5, repo.GetAll().Count());
        }

        [Test]
        public void CanAddContact()
        {
            var repo = new ContactsRepositoryMock();

            Contact model = new Contact();

            model.ContactName = "Test";
            model.EmailAddress = "test@testoncta.com";
            model.PhoneNumber = "505-555-555";
            model.Message = "Test Contact";

            repo.AddContact(model);

            Assert.AreEqual(6, repo.GetAll().Count());
        }

        [Test]
        public void CanLoadPurchases()
        {
            var repo = new PurchasesRepositoryMock();

            var sales = repo.GetSales(new SalesReportSearchParameters { FromDate = null, ToDate = null, UserName = "test" }).ToList();

            Assert.AreEqual(2, sales.Count());

        }

        [Test]
        public void CanAddPurchase()
        {
            var repo = new PurchasesRepositoryMock();

            Purchase model = new Purchase();

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

            repo = new PurchasesRepositoryMock();

            var sales = repo.GetSales(new SalesReportSearchParameters { FromDate = null, ToDate = null, UserName = "test" }).ToList();

            Assert.IsNotEmpty(sales);

        }

        [Test]
        public void CanLoadFeaturedVehicles()
        {

            var repo = new VehiclesRepositoryMock();

            repo.GenerateFeaturedVehicles();

            var featuredVehicles = repo.GetFeaturedVehicles().ToList();

            Assert.IsNotEmpty(featuredVehicles);

        }

        [Test]
        public void CanLoadInventoryReport()
        {
            InventoryReport model = new InventoryReport();

            var repo = new VehiclesRepositoryMock();


            var inventoryReports = repo.GetInventory(1).ToList();

            Assert.IsNotEmpty(inventoryReports);

            //Assert.AreEqual(2, inventoryReports[3].Count);


        }

        [Test]
        public void CanSelectInventoryNew()
        {


            var repo = new VehiclesRepositoryMock();


            var vehicles = repo.SelectInventory(new InventorySearchParameters { QuickSearch = null, TypeName = "new", MinPrice = null, MaxPrice = null, MinYear = null, MaxYear = null }).ToList();


            Assert.IsNotEmpty(vehicles);






        }

        [Test]
        public void CanAddVehicle()
        {
            Vehicle model = new Vehicle();

            var repo = new VehiclesRepositoryMock();


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


            var vehicles = repo.GetVehicleItems();

            Assert.AreEqual(21,repo.output.Count());

            //Assert.AreEqual(11, model.VehicleID);

        }

        [Test]
        public void CanUpdateVehicle()
        {

            var repo = new VehiclesRepositoryMock();

            var model = repo.output.FirstOrDefault(v => v.VehicleID == 1);


            model.TypeID = 2;
            model.Year = 2015;
            model.TransmissionID = 1;

            repo.UpdateVehicle(model);



            var updatedmodel = repo.SelectVehicleDetails(1);
            

            Assert.AreEqual(2015, updatedmodel.Year);

            //Assert.AreEqual(12999, updatedmodel.SalePrice);

        }

    }
}
