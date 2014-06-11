using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Validator;
using FluentValidation.Results;
using System.ComponentModel;
using System.Diagnostics;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.User;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersFormViewModel : PageViewModel, IDataErrorInfo
    {
        private IUserService _userService;
        private User _currentUser;

        public UsersFormViewModel()
            : base()
        {
            _userService = new UserServiceClient();

            _currentUser = new User();

            SaveUserCommand = new RelayCommand(this.SaveUser,
                () => IsValid);
        }

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (value != _currentUser)
                {
                    _currentUser = value;
                    RaisePropertyChanged("CurrentUser");
                }
            }
        }

        public ICommand SaveUserCommand
        {
            get;
            private set;
        }

        private void SaveUser()
        {
            UserValidator validator = new UserValidator();
            ValidationResult results = validator.Validate(_currentUser);

            if (!results.IsValid)
            {
                foreach (var failure in results.Errors)
                {
                    Console.WriteLine("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage);
                }
            }
            else
            {
                _userService.Save(_currentUser);
                RaisePropertyChanged("CurrentUser");
            }
        }

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public ValidationResult SelfValidate()
        {
            var r = ValidationHelper.Validate<UsersFormValidator, UsersFormViewModel>(this);
            foreach (var er in r.Errors)
            {
                Debug.WriteLine(er.ErrorMessage);
            }
            return r;
        }

        #endregion

        #region IDataErrorInfo Members
        public string Error
        {
            get { 
                var r = ValidationHelper.GetError(SelfValidate());
                return r;
            }
        }

        public string this[string columnName]
        {
            get
            {
                var __ValidationResults = SelfValidate();
                if (__ValidationResults == null) return string.Empty;
                var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
                return __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
            }
        }
        #endregion
    }
}
