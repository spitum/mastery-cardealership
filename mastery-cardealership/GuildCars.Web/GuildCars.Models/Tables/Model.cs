using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Model
    {
        public int ModelID { get; set; }
        public string ModelName { get; set; }
        public string ID { get; set; }

        public string Email { get; set; }

        public int MakeID { get; set; }

        public string MakeName { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
