using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentValidation.Validators;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;

namespace Arvato.TestProject.UsrMgmt.Entity.Validator
{
    public class UsersFormValidator : AbstractValidator<UsersFormViewModel>, IValidator<UsersFormViewModel>
    {
        public UsersFormValidator()
        {
            var uv = new UserValidator();
            uv.RuleFor(x => x.LoginID).SetValidator(new LoginIDMustNotExist());
            RuleFor(vm => vm.CurrentUser).SetValidator(uv);
            
            //RuleFor(vm => vm.CurrentUser.LoginID).NotNull().SetValidator(new LoginIDMustNotExist());
        }

        private class LoginIDMustNotExist : PropertyValidator
        {

            public LoginIDMustNotExist()
                : base("{PropertyName} already exists.")
            {

            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                var user = ((User)context.Instance);
                if (String.IsNullOrEmpty(user.LoginID))
                {
                    return false;
                }
                IUserService us = new UserService();
                return !us.IsExistingLoginID(user.LoginID, user.ID);
            }
        }
    }
}
