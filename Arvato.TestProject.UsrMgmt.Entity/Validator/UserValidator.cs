using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentValidation.Validators;

namespace Arvato.TestProject.UsrMgmt.Entity.Validator
{
    public class UserValidator : AbstractValidator<User>, IValidator<User>
    {
        public UserValidator()
        {
            RuleFor(user => user.FirstName).NotEmpty();
            RuleFor(user => user.LastName).NotEmpty();
            RuleFor(user => user.Email).NotEmpty().EmailAddress();
            RuleFor(user => user.LoginID).NotEmpty().Length(6, 50);
            RuleFor(user => user.Password).NotEmpty().Length(8, 50).When(user => user.IsWindowAuthenticate);
        }
    }
}
