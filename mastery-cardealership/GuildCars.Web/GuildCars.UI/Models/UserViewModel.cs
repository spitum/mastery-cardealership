using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Models
{
    public class UserViewModel
    {
        public ApplicationUser User { get; set; }
        public SelectList Roles { get; set; }
    }
}