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
    public class LoginFormValidator : AbstractValidator<LoginViewModel>, IValidator<LoginViewModel>
    {
        public LoginFormValidator()
        {
            RuleFor(x => x.LoginID).NotEmpty();
            RuleFor(x => x.Password).NotEmpty();
        }
    }
}
