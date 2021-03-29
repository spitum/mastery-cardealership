using GuildCars.Data.Interfaces;
using GuildCars.UI.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class VehiclesController : Controller
    {
        IVehiclesRepository _vehiclesRepository;
        public VehiclesController(IVehiclesRepository vehiclesRepository)
        {
            _vehiclesRepository = vehiclesRepository;
        }

        public ActionResult InventoryNew()
        {
            return View();
        }

        public ActionResult InventoryUsed()
        {
            return View();
        }

        public ActionResult VehicleDetails(int id)
        {
            var model = _vehiclesRepository.SelectVehicleDetails(id);
            return View(model);
        }


    }
}