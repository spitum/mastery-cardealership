using FluentValidation;
using GuildCars.Models.Queries;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Validations
{
    public class PurchaseAddViewModelValidator : AbstractValidator<PurchaseAddViewModel>
    {
       public PurchaseAddViewModelValidator()
        {
            RuleFor(m => m.Purchase.Name).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("Name is a required field.").MinimumLength(2).WithMessage("Name should be a minimum of 2 characters.");
            RuleFor(m => m.Purchase.StreetAddress1).NotEmpty().WithMessage("Street Address is required");
            RuleFor(m => m.Purchase.ZipCode.ToString()).Length(5, 5).WithMessage("Zip Code should be 5 digits");
            RuleFor(m => m.Purchase.PhoneNumber).NotEmpty().When(m => EmailPopulated(m)).WithMessage("Email address is required.");
            RuleFor(m => m.Purchase.EmailAddress).NotEmpty().When(m => PhonePopulated(m)).WithMessage("Phone number is required.");
            RuleFor(m => m.Purchase.EmailAddress).EmailAddress().WithMessage("Invalid email.");
            RuleFor(m => m.Purchase.PurchasePrice).NotEmpty().WithMessage("Purchase Price is a required field.");
        }
        // Breaking out some of the logic into separate reusable methods can help with readability.
        private bool EmailPopulated(PurchaseAddViewModel model)
        {
          return string.IsNullOrEmpty(model.Purchase.EmailAddress);
        }

        private bool PhonePopulated(PurchaseAddViewModel model)
        {
            return string.IsNullOrEmpty(model.Purchase.PhoneNumber);
        }

    }
}