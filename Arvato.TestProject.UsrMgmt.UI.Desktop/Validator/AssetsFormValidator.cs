using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentValidation;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentValidation.Validators;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using Arvato.TestProject.UsrMgmt.BLL.Interface;

namespace Arvato.TestProject.UsrMgmt.Entity.Validator
{
    public class AssetsFormValidator : AbstractValidator<AssetsFormViewModel>, IValidator<AssetsFormViewModel>
    {
        public AssetsFormValidator()
        {
            var uv = new AssetValidator();
            RuleFor(vm => vm.CurrentAsset).SetValidator(uv);
        }
    }
}
