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
    public class BookingsFormValidator : AbstractValidator<BookingsFormViewModel>, IValidator<BookingsFormViewModel>
    {
        public BookingsFormValidator()
        {
            RuleFor(x => x.Room).NotNull().WithMessage("Please select a room.");
            RuleFor(x => x.EndDate).Must(BeInTheFuture);
        }

        private bool BeInTheFuture(BookingsFormViewModel vm, DateTime end)
        {
            DateTime startDate = vm.StartDate.Add(vm.StartTime);
            DateTime endDate = vm.EndDate.Add(vm.EndTime);
            return endDate > startDate;
        }
    }
}
