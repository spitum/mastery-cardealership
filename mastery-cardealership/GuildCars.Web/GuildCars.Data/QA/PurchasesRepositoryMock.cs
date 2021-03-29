using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bogus;

namespace GuildCars.Data.QA
{
    public class PurchasesRepositoryMock : IPurchasesRepository
    {
        private static int id = 1;
        public static Faker<Purchase> faker = new Faker<Purchase>().RuleFor(c => c.VehicleID, f => id++)
                                                            .RuleFor(c => c.UserID, f=> f.PickRandom("00000000-0000-0000-0000-000000000000", "11111111-1111-1111-1111-111111111111"))
                                                            .RuleFor(c => c.Name, f => f.Person.FullName)
                                                            .RuleFor(c => c.EmailAddress, f => f.Person.Email)
                                                            .RuleFor(c => c.PhoneNumber, f => f.Person.Phone)
                                                            .RuleFor(c => c.StreetAddress1, f => f.Address.StreetAddress())
                                                            .RuleFor(c => c.StreetAddress2, f => f.Address.SecondaryAddress())
                                                            .RuleFor(c => c.City, f => f.Address.City())
                                                            .RuleFor(c => c.ZipCode, f => f.Random.Int(10500, 99999))
                                                            .RuleFor(c => c.PurchasePrice, f => f.Finance.Random.Decimal(1000M, 100000M))
                                                            .RuleFor(c => c.PurchaseTypeID, f => f.Random.Int(1, 3))
                                                            .RuleFor(c => c.PurchaseDate, f => f.Date.Past());

        public List<Purchase> output = faker.Generate(5);


        List<MockUsers> users = new List<MockUsers> { new MockUsers { UserID = "00000000-0000-0000-0000-000000000000", UserName = "test" }, new MockUsers { UserID = "11111111-1111-1111-1111-111111111111", UserName = "test1" } };


        public void AddPurchase(Purchase purchase)
        {
            purchase.UserID = "00000000-0000-0000-0000-000000000000";
            output.Add(purchase);
        }

        public List<SalesReport> GetSales(SalesReportSearchParameters parameters)
        {
            var result = new List<SalesReport>();

            var salesByUser = (from o in output
                               join u in users on o.UserID equals u.UserID
                               select new
                               {
                                   o.UserID,
                                   u.UserName,
                                   o.PurchasePrice,
                                   o.PurchaseDate,
                                   o.VehicleID
                               }).ToList();



            if(parameters.UserName != null && parameters.FromDate != null && parameters.ToDate != null)
            {
                salesByUser = (from s in salesByUser
                              where s.UserName.Contains(parameters.UserName) || (s.PurchaseDate >= parameters.FromDate && s.PurchaseDate <= parameters.ToDate)
                              select s).ToList();
                              
            }

            if (parameters.UserName == null && parameters.FromDate != null && parameters.ToDate != null)
            {
                salesByUser = (from s in salesByUser
                              where s.PurchaseDate >= parameters.FromDate && s.PurchaseDate <= parameters.ToDate
                              select s).ToList();
            }

            if (parameters.UserName != null && parameters.FromDate == null && parameters.ToDate != null)
            {
                salesByUser = (from s in salesByUser
                               where s.UserName.Contains(parameters.UserName) && s.PurchaseDate <= parameters.ToDate
                               select s).ToList();
            }

            if (parameters.UserName == null && parameters.FromDate != null && parameters.ToDate == null)
            {
                salesByUser = (from s in salesByUser
                               where s.PurchaseDate >= parameters.FromDate
                               select s).ToList();
            }

            if (parameters.UserName != null && parameters.FromDate == null && parameters.ToDate == null)
            {
                salesByUser = (from s in salesByUser
                               where  s.UserName.Contains(parameters.UserName)
                               select s).ToList();
            }


            var groupsalesByUser = (from s in salesByUser
                                    group s by new { s.UserID, s.UserName } into sel
                                    select new SalesReport()
                                    {
                                        UserID = sel.Key.UserID,
                                        UserName = sel.Key.UserName,
                                        TotalSales = sel.Sum(p => p.PurchasePrice),
                                        TotalVehicles = sel.Count()
                                    }).ToList();

            return groupsalesByUser;

        }
    }


}
