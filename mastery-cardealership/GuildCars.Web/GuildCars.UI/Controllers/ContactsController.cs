using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using GuildCars.UI.Core;
using GuildCars.UI.GoogleCaptcha;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GuildCars.UI.Controllers
{
    public class ContactsController : Controller
    {
        IContactsRepository _contactsRepository;
        public ContactsController(IContactsRepository contactsRepository)
        {
            _contactsRepository = contactsRepository;
        }
        // GET: Contacts
        public ActionResult AddContact()
        {
            var contact = new Contact();
            return View(contact);
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateGoogleCaptcha]
        public ActionResult AddContact(Contact contact)
        {
            if (ModelState.IsValid)
            {
                _contactsRepository.AddContact(contact);
                return RedirectToAction("Index", "Home");
            }
            return View(contact);

        }
    }
}