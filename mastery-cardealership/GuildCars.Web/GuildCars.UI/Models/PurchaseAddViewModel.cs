using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Models.Validations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class PurchaseAddViewModel 
    {
        public Purchase Purchase { get; set; }

        public IEnumerable<SelectListItem> States { get; set; }

        public IEnumerable<SelectListItem> PurchaseTypes { get; set; }
    }
}