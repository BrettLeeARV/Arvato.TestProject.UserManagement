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

    public class RoomsFormValidator : AbstractValidator<RoomsFormViewModel>, IValidator<RoomsFormViewModel>
    {
        public RoomsFormValidator()
        {
            var uv = new RoomValidator();
            RuleFor(vm => vm.CurrentRoom).SetValidator(uv);
        }

    }

}
