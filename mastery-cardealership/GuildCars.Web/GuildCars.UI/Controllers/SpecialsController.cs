using GuildCars.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class SpecialsController : Controller
    {
        ISpecialsRepository _specialsRepository;
        public SpecialsController(ISpecialsRepository specialsRepository)
        {
            _specialsRepository = specialsRepository;
        }
        // GET: Specials
        public ActionResult Index()
        {
            var model = _specialsRepository.GetAll();
            return View(model);
        }
    }
}