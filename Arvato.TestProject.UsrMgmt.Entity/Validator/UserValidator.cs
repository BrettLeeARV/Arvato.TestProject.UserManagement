using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;

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
            // not existing loginID
            // is a valid AD loginID
            // password is required and longer than 8 chars, if not Windows Authenticate
        }
    }
}
