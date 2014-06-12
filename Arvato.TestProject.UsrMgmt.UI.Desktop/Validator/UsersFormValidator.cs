﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentValidation.Validators;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.User;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.LDAP;

namespace Arvato.TestProject.UsrMgmt.Entity.Validator
{
    public class UsersFormValidator : AbstractValidator<UsersFormViewModel>, IValidator<UsersFormViewModel>
    {
        public UsersFormValidator()
        {
            //var uv = new UserValidator();
            //uv.RuleFor(x => x.LoginID).SetValidator(new LoginIDMustNotExist());
            //RuleFor(vm => vm.CurrentUser).SetValidator(uv);
            
            RuleFor(vm => vm.LoginID).NotNull().SetValidator(new LoginIDMustNotExist());
            RuleFor(user => user.LoginID).Must((u, user) => { return IsValidWindowLoginID(u.IsWindowAuthenticate, u.LoginID); });
        }

        private bool IsValidWindowLoginID(bool IsWindowAuthenticate, string LoginID)
        {
            if (LoginID != null && LoginID.Trim().Length > 0)
            {
                ILDAPService ADService = new LDAPServiceClient();
                User u = new User() { LoginID = LoginID };
                if (IsWindowAuthenticate == true && ADService.IsExistUser(u) == false)
                    return false;
                else
                    return true;
            }
            else
            {
                return true;
            }
        }

        private class LoginIDMustNotExist : PropertyValidator
        {

            public LoginIDMustNotExist()
                : base("{PropertyName} already exists.")
            {

            }

            protected override bool IsValid(PropertyValidatorContext context)
            {
                var user = (context.Instance as UsersFormViewModel).CurrentUser;
                if (String.IsNullOrEmpty(user.LoginID))
                {
                    return false;
                }
                IUserService us = new UserServiceClient();
                return !us.IsExistingLoginID(user.LoginID, user.ID);
            }
        }
    }
}
