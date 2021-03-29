using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class VehicleItem
    {
        public int VehicleID { get; set; }
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int TypeID { get; set; }
        public string TypeName { get; set; }
        public int StyleID { get; set; }
        public string BodyStyleName { get; set; }
        public int Year { get; set; }
        public int TransmissionID { get; set; }
        public string TransmissionName { get; set; }
        public int ColorID { get; set; }
        public string ColorName { get; set; }
        public int InteriorColorID { get; set; }
        public string InteriorColorName { get; set; }
        public int Mileage { get; set; }
        public string VINNumber { get; set; }
        public decimal? MSRP { get; set; }
        public decimal? SalePrice { get; set; }
        public string ImageFileName { get; set; }
        public string Description { get; set; }
        public bool Featured { get; set; } = false;
    }
}
