using GuildCars.Models.Tables;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class VehicleAddViewModel
    {
        public Vehicle Vehicle { get; set; }
        public IEnumerable<SelectListItem> Makes { get; set; }

        public IEnumerable<SelectListItem> Models { get; set; }

        public IEnumerable<SelectListItem> Types { get; set; }

        public IEnumerable<SelectListItem> BodyStyles { get; set; }

        public IEnumerable<SelectListItem> Transmissions { get; set; }

        public IEnumerable<SelectListItem> Colors { get; set; }

        public IEnumerable<SelectListItem> InteriorColors { get; set; }
        public HttpPostedFileBase ImageUpload { get; set; }
    }
}