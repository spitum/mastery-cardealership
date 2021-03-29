using GuildCars.Data.Interfaces;
using GuildCars.Models.Tables;
using GuildCars.UI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using GuildCars.UI.Utilities;
using GuildCars.UI.Models.Validations;
using FluentValidation.Mvc;
using PayPal.Api;

namespace GuildCars.UI.Controllers
{
    [Authorize(Roles="Sales")]
    public class SalesController : Controller
    {
        IPurchasesRepository _purchasesRepository;
        IPurchaseTypesRepository _purchasesTypeRepository;
        IStatesRepository _statesRepository;
        IVehiclesRepository _vehiclesRepository;
        public SalesController(IPurchasesRepository purchasesRepository, IStatesRepository statesRepository, IPurchaseTypesRepository purchasesTypeRepository,IVehiclesRepository vehiclesRepository)
        {
            _purchasesRepository = purchasesRepository;
            _purchasesTypeRepository = purchasesTypeRepository;
            _statesRepository = statesRepository;
            _vehiclesRepository = vehiclesRepository;
        }
        // GET: Sales
        public ActionResult Index()
        {
            return View();
        }

        [Authorize]
        public ActionResult AddPurchase(int id)
        {
            var model = new PurchaseViewModels();
            model.vehicle = _vehiclesRepository.SelectVehicleDetails(id);

            model.purchase = new PurchaseAddViewModel();
            model.purchase.States = new SelectList(_statesRepository.GetAll(), "StateID", "StateID");
            model.purchase.PurchaseTypes = new SelectList(_purchasesTypeRepository.GetAll(), "PurchaseTypeID", "PurchaseTypeName");

            model.purchase.Purchase = new Purchase();

            model.purchase.Purchase.VehicleID = model.vehicle.VehicleID;
            Session["VIN"] = model.vehicle.VINNumber;

            return View(model);
        }

        [Authorize]
        [ValidateAntiForgeryToken]
        [HttpPost]
        public ActionResult AddPurchase(PurchaseViewModels model)
        {
            if (ModelState.IsValid)
            {
                model.purchase.Purchase.UserID = AuthorizeUtilities.GetUserId(this);

                var validator = new PurchaseAddViewModelValidator();
                var result = validator.Validate(model.purchase);
                try
                {
                    if (result.IsValid)
                    {

                        _purchasesRepository.AddPurchase(model.purchase.Purchase);
                        TempData["notice"] = "Purchase successfully added";

                        //Save purchase to session for paypal checkout
                        Session["purchase"] = model.purchase.Purchase;

                        return RedirectToAction("CheckOut","Sales");

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

            model.vehicle = _vehiclesRepository.SelectVehicleDetails(model.purchase.Purchase.VehicleID);
            model.purchase.States = new SelectList(_statesRepository.GetAll(), "StateID", "StateID");
            model.purchase.PurchaseTypes = new SelectList(_purchasesTypeRepository.GetAll(), "PurchaseTypeID", "PurchaseTypeName");
            return View(model);

        }
  
        public ActionResult Checkout()
        {
           
            return View("CheckOut");
        }

        private Payment CreatePayment(APIContext apiContext, string redirectUrl)
        {
            var listItems = new ItemList() { items = new List<Item>() };

            Purchase purchase = Session["purchase"] as Purchase;
            var VIN = Session["VIN"];
            //Adding Item Details like name, currency, price etc  
            listItems.items.Add(new Item()
            {
                name = VIN.ToString(),
                currency = "USD",
                price = purchase.PurchasePrice.ToString(),
                quantity = "1",
                sku = "sku"
            });
            var payer = new Payer()
            {
                payment_method = "paypal"
            };
            // Configure Redirect Urls here with RedirectUrls object  
            var redirUrls = new RedirectUrls()
            {
                cancel_url = redirectUrl + "&Cancel=true",
                return_url = redirectUrl
            };
            // Adding Tax, shipping and Subtotal details  
            var details = new Details()
            {
                tax = (Convert.ToDouble(purchase.PurchasePrice * 0.032M)).ToString(),
                shipping = "1",
                subtotal = purchase.PurchasePrice.ToString()
            };
            //Final amount with details  
            var amount = new Amount()
            {
                currency = "USD",
                total = (Convert.ToDouble(details.tax) + Convert.ToDouble(details.shipping) + Convert.ToDouble(details.subtotal)).ToString(), // Total must be equal to sum of tax, shipping and subtotal.  
                details = details
            };
            var transactionList = new List<Transaction>();
            // Adding description about the transaction  
            transactionList.Add(new Transaction()
            {
                description = "Vehicle Purchase order",
                invoice_number = Convert.ToString((new Random()).Next(100000)), //Generate an Invoice No  
                amount = amount,
                item_list = listItems
            });
            this.payment = new Payment()
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirUrls
            };
            // Create a payment using a APIContext  
            return this.payment.Create(apiContext);
        }

        public ActionResult PaymentWithPaypal(string Cancel = null)
        {
            //getting the apiContext  
            APIContext apiContext = PaypalConfiguration.GetAPIContext();
            try
            {
                //A resource representing a Payer that funds a payment Payment Method as paypal  
                //Payer Id will be returned when payment proceeds or click to pay  
                string payerId = Request.Params["PayerID"];
                if (string.IsNullOrEmpty(payerId))
                {
                    //this section will be executed first because PayerID doesn't exist  
                    //it is returned by the create function call of the payment class  
                    // Creating a payment  
                    // baseURL is the url on which paypal sendsback the data.  
                    string baseURI = Request.Url.Scheme + "://" + Request.Url.Authority + "/Sales/PaymentWithPayPal?";
                    //here we are generating guid for storing the paymentID received in session  
                    //which will be used in the payment execution  
                    var guid = Convert.ToString((new Random()).Next(100000));
                    //CreatePayment function gives us the payment approval url  
                    //on which payer is redirected for paypal account payment  
                    var createdPayment = this.CreatePayment(apiContext, baseURI + "guid=" + guid);
                    //get links returned from paypal in response to Create function call  
                    var links = createdPayment.links.GetEnumerator();
                    string paypalRedirectUrl = null;
                    while (links.MoveNext())
                    {
                        Links lnk = links.Current;
                        if (lnk.rel.ToLower().Trim().Equals("approval_url"))
                        {
                            //saving the payapalredirect URL to which user will be redirected for payment  
                            paypalRedirectUrl = lnk.href;
                        }
                    }
                    // saving the paymentID in the key guid  
                    Session.Add(guid, createdPayment.id);
                    return Redirect(paypalRedirectUrl);
                }
                else
                {
                    // This function exectues after receving all parameters for the payment  
                    var guid = Request.Params["guid"];
                    var executedPayment = ExecutePayment(apiContext, payerId, Session[guid] as string);
                    //If executed payment failed then we will show payment failure message to user  
                    if (executedPayment.state.ToLower() != "approved")
                    {
                        return View("Failure");
                    }
                }
            }
            catch (Exception ex)
            {
                PaypalLogger.Log("Error: " + ex.Message);
                return View("Failure");
            }
            //on successful payment, show success page to user.  
            return View("Success");
        }

        private PayPal.Api.Payment payment;
        private Payment ExecutePayment(APIContext apiContext, string payerId, string paymentId)
        {
            var paymentExecution = new PaymentExecution()
            {
                payer_id = payerId
            };
            this.payment = new Payment()
            {
                id = paymentId
            };
            return this.payment.Execute(apiContext, paymentExecution);
        }

    }
}

