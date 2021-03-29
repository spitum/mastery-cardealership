using FluentValidation.Mvc;
using GuildCars.Data.Interfaces;
using GuildCars.Models.Queries;
using GuildCars.Models.Tables;
using GuildCars.UI.Core;
using GuildCars.UI.Models;
using GuildCars.UI.Models.Validations;
using GuildCars.UI.Utilities;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.Owin.Security;

namespace GuildCars.UI.Controllers
{

    public class AdminController : Controller
    {
        IVehiclesRepository _vehiclesRepository;
        IMakesRepository _makesRepository;
        IModelsRepository _modelsRepository;
        ITypesRepository _typesRepository;
        IBodyStylesRepository _bodyStylesRepository;
        ITransmissionsRepository _transmissionsRepository;
        IColorsRepository _colorsRepository;
        IInteriorColorsRepository _interiorColorsRepository;
        ISpecialsRepository _specialsRepository;

        ApplicationDbContext _context;

        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AdminController(IVehiclesRepository vehiclesRepository
                                , IMakesRepository makesRepository, IModelsRepository modelsRepository
                                , ITypesRepository typesRepository, IBodyStylesRepository bodyStylesRepository
                                , ITransmissionsRepository transmissionsRepository, IColorsRepository colorsRepository, IInteriorColorsRepository interiorColorsRepository, ISpecialsRepository specialsRepository
                                )
        {
            _vehiclesRepository = vehiclesRepository;
            _makesRepository = makesRepository;
            _modelsRepository = modelsRepository;
            _typesRepository = typesRepository;
            _bodyStylesRepository = bodyStylesRepository;
            _transmissionsRepository = transmissionsRepository;
            _colorsRepository = colorsRepository;
            _interiorColorsRepository = interiorColorsRepository;
            _specialsRepository = specialsRepository;
            _context = new ApplicationDbContext();
        }
        // GET: Admin

        [Authorize(Roles = "Admin")]
        public ActionResult Index()
        {
            return View();
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddVehicle()
        {
            var model = new VehicleAddViewModel();

            model.Makes = new SelectList(_makesRepository.GetAll(), "MakeID", "MakeName");
            model.Models = new SelectList(_modelsRepository.GetAll(), "ModelID", "ModelName");
            model.Types = new SelectList(_typesRepository.GetAll(), "TypeID", "TypeName");
            model.BodyStyles = new SelectList(_bodyStylesRepository.GetAll(), "StyleID", "BodyStyleName");
            model.Transmissions = new SelectList(_transmissionsRepository.GetAll(), "TransmissionID", "TransmissionName");
            model.Colors = new SelectList(_colorsRepository.GetAll(), "ColorID", "ColorName");
            model.InteriorColors = new SelectList(_interiorColorsRepository.GetAll(), "InteriorColorID", "InteriorColorName");

            model.Vehicle = new Vehicle();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddVehicle(VehicleAddViewModel model)
        {
            if (ModelState.IsValid)
            {
                var validator = new AddVehicleViewModelValidator();
                var result = validator.Validate(model);

                try
                {
                    if (result.IsValid)
                    {

                        var parameters = new InventorySearchParameters()
                        {
                            QuickSearch = null,
                            MaxPrice = null,
                            MinPrice = null,
                            MinYear = null,
                            MaxYear = null,
                            TypeName = null
                        };


                        var count = _vehiclesRepository.SelectInventory(parameters).Max(m => m.VehicleID) + 1;

                        if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                        {
                            var savePath = Server.MapPath("~/Images");
                            string fileName = "inventory-" + count;
                            string extension = Path.GetExtension(model.ImageUpload.FileName);
                            var filePath = Path.Combine(savePath, fileName + extension);

                            model.ImageUpload.SaveAs(filePath);

                            model.Vehicle.ImageFileName = Path.GetFileName(filePath);

                            _vehiclesRepository.AddVehicle(model.Vehicle);
                            TempData["notice"] = "Vehicle added successfully.";
                            return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleID });
                        }
                    }

                    else
                    {
                        result.AddToModelState(ModelState, null);
                    }


                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }


            model.Makes = new SelectList(_makesRepository.GetAll(), "MakeID", "MakeName");
            model.Models = new SelectList(_modelsRepository.GetAll(), "ModelID", "ModelName");
            model.Types = new SelectList(_typesRepository.GetAll(), "TypeID", "TypeName");
            model.BodyStyles = new SelectList(_bodyStylesRepository.GetAll(), "StyleID", "BodyStyleName");
            model.Transmissions = new SelectList(_transmissionsRepository.GetAll(), "TransmissionID", "TransmissionName");
            model.Colors = new SelectList(_colorsRepository.GetAll(), "ColorID", "ColorName");
            model.InteriorColors = new SelectList(_interiorColorsRepository.GetAll(), "InteriorColorID", "InteriorColorName");

            return View(model);

        }
        [Authorize(Roles = "Admin")]
        public ActionResult EditVehicle(int id)
        {
            var model = new VehicleEditViewModel();

            model.Makes = new SelectList(_makesRepository.GetAll(), "MakeID", "MakeName");
            model.Models = new SelectList(_modelsRepository.GetAll(), "ModelID", "ModelName");
            model.Types = new SelectList(_typesRepository.GetAll(), "TypeID", "TypeName");
            model.BodyStyles = new SelectList(_bodyStylesRepository.GetAll(), "StyleID", "BodyStyleName");
            model.Transmissions = new SelectList(_transmissionsRepository.GetAll(), "TransmissionID", "TransmissionName");
            model.Colors = new SelectList(_colorsRepository.GetAll(), "ColorID", "ColorName");
            model.InteriorColors = new SelectList(_interiorColorsRepository.GetAll(), "InteriorColorID", "InteriorColorName");

            model.Vehicle = _vehiclesRepository.SelectVehicleByID(id);

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [MultipleButton(Name = "action", Argument = "EditVehicle")]
        public ActionResult EditVehicle(VehicleEditViewModel model)
        {
            var oldVehicle = _vehiclesRepository.SelectVehicleByID(model.Vehicle.VehicleID);
            if (ModelState.IsValid)
            {
                var validator = new EditVehicleViewModelValidator();
                var result = validator.Validate(model);
                try
                {

                    if (result.IsValid)
                    {

                        if (model.ImageUpload != null && model.ImageUpload.ContentLength > 0)
                        {
                            var savePath = Server.MapPath("~/Images");

                            string fileName = Path.GetFileNameWithoutExtension(model.ImageUpload.FileName);
                            string extension = Path.GetExtension(model.ImageUpload.FileName);
                            var filePath = Path.Combine(savePath, fileName + extension);

                            int counter = 1;
                            while (System.IO.File.Exists(filePath))
                            {
                                filePath = Path.Combine(savePath, fileName + counter.ToString() + extension);
                                counter++;
                            }

                            model.ImageUpload.SaveAs(filePath);

                            model.Vehicle.ImageFileName = Path.GetFileName(filePath);

                            //old file - delete
                            var oldPath = Path.Combine(savePath, oldVehicle.ImageFileName);

                            if (System.IO.File.Exists(oldPath))
                            {
                                System.IO.File.Delete(oldPath);
                            }

                        }

                        //keep old image file if edit image is null
                        else
                        {

                            model.Vehicle.ImageFileName = oldVehicle.ImageFileName;
                        }
                        
                        _vehiclesRepository.UpdateVehicle(model.Vehicle);
                        return RedirectToAction("EditVehicle", new { id = model.Vehicle.VehicleID });

                    }
                    else
                    {
                        result.AddToModelState(ModelState, null);
                    }
                }

                

                catch (Exception ex)
                {

                    throw ex;
                }
            }

            model.Makes = new SelectList(_makesRepository.GetAll(), "MakeID", "MakeName");
            model.Models = new SelectList(_modelsRepository.GetAll(), "ModelID", "ModelName");
            model.Types = new SelectList(_typesRepository.GetAll(), "TypeID", "TypeName");
            model.BodyStyles = new SelectList(_bodyStylesRepository.GetAll(), "StyleID", "BodyStyleName");
            model.Transmissions = new SelectList(_transmissionsRepository.GetAll(), "TransmissionID", "TransmissionName");
            model.Colors = new SelectList(_colorsRepository.GetAll(), "ColorID", "ColorName");
            model.InteriorColors = new SelectList(_interiorColorsRepository.GetAll(), "InteriorColorID", "InteriorColorName");
            model.Vehicle.ImageFileName = oldVehicle.ImageFileName;

            return View(model);
            
        }

        [Authorize]
        [HttpPost]
        [MultipleButton(Name = "action", Argument = "DeleteVehicle")]
        public ActionResult DeleteVehicle(int id)
        {
            var userid = AuthorizeUtilities.GetUserId(this);

            _vehiclesRepository.DeleteVehicle(id);

            return RedirectToAction("Index","Home");
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddMake()
        {
            var userid = AuthorizeUtilities.GetUserId(this);

            var model = new Make();

            model.ID = userid;

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddMake(Make model)
        {

            if (ModelState.IsValid)
            {
                _makesRepository.AddMake(model);
                return RedirectToAction("AddMake", "Admin");
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddModel()
        {
            var userid = AuthorizeUtilities.GetUserId(this);

            var model = new Model();
            model.ID = userid;

            ViewBag.Name = new SelectList(_makesRepository.GetAll(), "MakeID", "MakeName");

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddModel(Model model)
        {
            if (ModelState.IsValid)
            {
                _modelsRepository.AddModel(model);
                return RedirectToAction("AddModel", "Admin");
            }

            ViewBag.Name = new SelectList(_makesRepository.GetAll(), "MakeID", "MakeName");
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult AddSpecial()
        {

            var model = new Special();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public ActionResult AddSpecial(Special model)
        {
            if (ModelState.IsValid)
            {
                _specialsRepository.AddSpecial(model);
                return RedirectToAction("AddSpecial", "Admin");
            }
            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public ActionResult Users()
        {

            List<UserViewModel> modellist = new List<UserViewModel>();
            var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(_context));
            if (userManager.Users.ToList().Count != 0)
            {
                userManager.Users.ToList().ForEach(u =>
                {
                    UserViewModel model = new UserViewModel();
                    model.User = u;
                    model.Roles = getallRoles(u.Roles.FirstOrDefault().RoleId);
                    modellist.Add(model);
                });
            }
            return View(modellist);
        }

        [AllowAnonymous]
        public ActionResult Register()
        {
            ViewBag.Name = new SelectList(_context.Roles
                                .ToList(), "Name", "Name");
            //ViewBag.UserRoles = new SelectList(_context.Roles.ToList(), "Id", "Name");

            return View();
        }


        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email,LastName = model.LastName,FirstName=model.FirstName };
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    // string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    // var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    // await UserManager.SendEmailAsync(user.Id, "Confirm your account", "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

                    //Assign Role to user Here       
                    await UserManager.AddToRoleAsync(user.Id, model.UserRoles);

                    return RedirectToAction("Index", "Home");
                }
                //ViewBag.UserRoles = new SelectList(_context.Roles.ToList(), "Id", "Name");
                ViewBag.Name = new SelectList(_context.Roles
                    .ToList(), "Name", "Name");
                AddErrors(result);
            }

            // If we got this far, something failed, redisplay form
            //ViewBag.UserRoles = new SelectList(_context.Roles.ToList(), "Name", "Name");
            ViewBag.Name = new SelectList(_context.Roles
            .ToList(), "Name", "Name");
            return View(model);
        }

        public SelectList getallRoles(string selectedRoleId = "1")
        {
            //ApplicationDbContext context = new ApplicationDbContext();

            //var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));

            return new SelectList(_context.Roles.ToList(), "Id", "Name", selectedRoleId);
        }

        [Authorize(Roles = "Admin")]
        public ActionResult UpdateUser(string userID)
        {
            var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(_context));
            var roleManager = new RoleManager<IdentityRole, string>(new RoleStore<IdentityRole>(_context));

            var model = new UpdateUserViewModel();

            model.User = userManager.FindById(userID);

            model.Role = roleManager.FindById(model.User.Roles.FirstOrDefault().RoleId).Name;


            ViewBag.Name = new SelectList(_context.Roles
                                .ToList(), "Name", "Name");

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> UpdateUser(UpdateUserViewModel model)

        {
            if (ModelState.IsValid)
            {
                //var userManager = new UserManager<ApplicationUser, string>(new UserStore<ApplicationUser>(_context));
                var store = new UserStore<ApplicationUser>(_context);
                var roleManager = new RoleManager<IdentityRole, string>(new RoleStore<IdentityRole>(_context));
                //get current user and update
                var user = await UserManager.FindByIdAsync(model.User.Id);
                user.FirstName = model.User.FirstName;
                user.LastName = model.User.LastName;

                if (model.Password != null && (model.Password == model.ConfirmPassword))
                {

                    UserManager.RemovePassword(user.Id);

                    var newPasswordHash = UserManager.PasswordHasher.HashPassword(model.Password);
                    //UserManager.FindById(user.Id);
                    await store.SetPasswordHashAsync(UserManager.FindById(user.Id), newPasswordHash);

                }


                var oldrole = roleManager.FindById(user.Roles.FirstOrDefault().RoleId);

                UserManager.RemoveFromRole(user.Id, oldrole.Name);

                UserManager.AddToRole(user.Id, model.Role);

                var updateResult = await UserManager.UpdateAsync(user);
                if (updateResult.Succeeded)
                {
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                    return RedirectToAction("Users", "Admin");
                }
                ViewBag.Name = new SelectList(_context.Roles
                .ToList(), "Name", "Name");
                AddErrors(updateResult);
            }
            //failed - do something else and return
            ViewBag.Name = new SelectList(_context.Roles
           .ToList(), "Name", "Name");
            return View(model);
        }

        [Authorize(Roles = "Admin,Sales")]
        // GET: /Admin/ChangePassword
        public ActionResult ChangePassword()
        {
            var model = new AdminChangePasswordViewModel();
            return View(model);
        }

        [Authorize(Roles = "Admin")]
        // POST: /Admin/ChangePassword
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ChangePassword(AdminChangePasswordViewModel model)
        {
            var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());
            var store = new UserStore<ApplicationUser>(_context);

            if (ModelState.IsValid)
            {



                if (model.NewPassword != null && (model.NewPassword == model.ConfirmPassword))
                {

                    //UserManager.RemovePassword(user.Id);
                    var newPasswordHash = UserManager.PasswordHasher.HashPassword(model.NewPassword);
                    //UserManager.FindById(user.Id);
                    await store.SetPasswordHashAsync(UserManager.FindById(user.Id), newPasswordHash);

                }

                var result = await UserManager.UpdateAsync(user);
                if (result.Succeeded)
                {
                    var updatedUser = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                    if (updatedUser != null)
                    {
                        await SignInManager.SignInAsync(updatedUser, isPersistent: false, rememberBrowser: false);
                    }
                    return RedirectToAction("Users", "Admin");
                }

                AddErrors(result);
                return View(model);
            }

            return View(model);
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

    }




 
}


