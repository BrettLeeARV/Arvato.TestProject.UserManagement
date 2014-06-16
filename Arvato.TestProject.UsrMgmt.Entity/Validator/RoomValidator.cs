using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentValidation.Validators;

namespace Arvato.TestProject.UsrMgmt.Entity.Validator
{
    public class RoomValidator : AbstractValidator<Room>, IValidator<Room>
    {
        public RoomValidator()
        {
            RuleFor(room => room.Name).NotEmpty();
            RuleFor(room => room.Location).NotEmpty();
            RuleFor(room => room.Capacity).GreaterThan(0).WithMessage("Capacity must be greater than 0.");
            //RuleFor(room => room.Capacity).Must(BeNumeric).WithMessage("");
        }

        static bool BeNumeric(string propertyValue)
        {
            int result;
            return int.TryParse(propertyValue, out result);
        }
    }
}
