using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GuildCars.UI.Models.Validations
{
    public class EditFileValidator : AbstractValidator<HttpPostedFileBase>
    {
        public EditFileValidator()
        {

            RuleFor(x => x.FileName.Length).NotNull().LessThanOrEqualTo(100)
                .WithMessage("File size is larger than allowed");

            RuleFor(x => x.ContentType).NotNull().Must(x => x.Equals("image/jpeg") || x.Equals("image/jpg") || x.Equals("image/png"))
                .WithMessage("Only jpeg / jpg / png file types are allowed.");

        }
    }
}