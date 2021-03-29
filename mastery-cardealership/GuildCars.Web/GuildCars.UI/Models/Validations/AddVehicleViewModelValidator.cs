using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Validations
{
    public class AddVehicleViewModelValidator : AbstractValidator<VehicleAddViewModel>
    {
        public AddVehicleViewModelValidator()
        {
            RuleFor(m => m.Vehicle.Description).NotEmpty().WithMessage("Description is required.");
            RuleFor(m => m.Vehicle.VINNumber).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("VIN Number is required.")
                .Matches("[a-zA-Z0-9]{9}[a-zA-Z0-9-]{2}[0-9]{6}").WithMessage("Invalid VIN Number");
            RuleFor(m => m.Vehicle.Mileage).InclusiveBetween(0, 1000).When(m => m.Vehicle.TypeID == 1).WithMessage("Mileage must be between 0 and 1000 for new vehicles.");
            RuleFor(m => m.Vehicle.Mileage).GreaterThan(1000).When(m => m.Vehicle.TypeID == 2).WithMessage("Mileage must be over 1000 for used vehicles.");
            RuleFor(m => m.Vehicle.MSRP).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("MSRP should be a positive number")
                .GreaterThanOrEqualTo(m => m.Vehicle.SalePrice).WithMessage("MSRP should be greater than the Sale Price.");
            RuleFor(m => m.Vehicle.SalePrice).Cascade(CascadeMode.StopOnFirstFailure).NotEmpty().WithMessage("Sale Price should be a positive number")
                .LessThanOrEqualTo(m => m.Vehicle.MSRP).WithMessage("Sale Price should be less than the MSRP");
            RuleFor(m => m.ImageUpload).SetValidator(new FileValidator());
            RuleFor(x => x.ImageUpload).NotEmpty().WithMessage("An image is required.");
        }

    }
}