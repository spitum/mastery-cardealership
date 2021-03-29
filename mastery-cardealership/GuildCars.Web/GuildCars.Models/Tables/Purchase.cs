using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Purchase
    {
        public int VehicleID { get; set; }
        public string UserID { get; set; }
        public string Name { get; set; }
        public string EmailAddress { get; set; }
        public string PhoneNumber { get; set; }

        public string StreetAddress1 { get; set; }
        public string StreetAddress2 { get; set; }
        public string City { get; set; }
        public string StateID { get; set; }
        public int ZipCode { get; set; }
        public decimal PurchasePrice { get; set; }
        public int PurchaseTypeID { get; set; }
        public DateTime PurchaseDate { get; set; }

    }
}
