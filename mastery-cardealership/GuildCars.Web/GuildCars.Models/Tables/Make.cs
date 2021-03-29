using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GuildCars.Models.Tables
{
    public class Make
    {
        public int MakeID { get; set; }
        public string MakeName { get; set; }

        public string ID { get; set; }
        public string Email { get; set; }

        public DateTime CreatedDate { get; set; }

    }
}
