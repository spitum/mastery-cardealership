using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class IndexViewModels
    {
        public IEnumerable<FeaturedVehicles> featured {get; set;}
        public IEnumerable<Special> specials { get; set; }
    }
}