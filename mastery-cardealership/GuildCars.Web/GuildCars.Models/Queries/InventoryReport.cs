using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Queries
{
    public class InventoryReport
    {
        public int MakeID { get; set; }
        public string MakeName { get; set; }
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public int  Year { get; set; }
        public int Count { get; set; }
        public decimal StockValue { get; set; }

        public int TypeID { get; set; }

        public string TypeName { get; set; }

    }
}
