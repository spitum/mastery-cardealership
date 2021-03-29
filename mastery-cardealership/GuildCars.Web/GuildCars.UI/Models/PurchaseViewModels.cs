using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models
{
    public class PurchaseViewModels
    {
        public VehicleItem vehicle { get; set; }
        public PurchaseAddViewModel purchase { get; set; }
    }
}