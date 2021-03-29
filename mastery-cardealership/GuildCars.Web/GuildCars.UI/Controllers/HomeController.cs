using GuildCars.Data;
using GuildCars.Data.Dapper;
using GuildCars.Data.Interfaces;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class HomeController : Controller
    {
        IVehiclesRepository _vehiclesRepository;
        ISpecialsRepository _specialsRepository;
        public HomeController(IVehiclesRepository vehiclesRepository, ISpecialsRepository specialsRepository)
        {
            _vehiclesRepository = vehiclesRepository;
            _specialsRepository = specialsRepository;
        }
        public ActionResult Index()
        {
            var model = new IndexViewModels();
            model.featured = _vehiclesRepository.GetFeaturedVehicles();
            model.specials = _specialsRepository.GetAll();
            return View(model);
        }

        public ActionResult WarningMessage()
        {
            return View();
        }


    }
}