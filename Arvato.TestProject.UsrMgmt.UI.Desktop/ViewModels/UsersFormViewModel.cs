using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Validator;
using FluentValidation.Results;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersFormViewModel : PageViewModel
    {
        private IUserService _userService;
        private User _currentUser;

        public UsersFormViewModel()
            : base()
        {
            _userService = new UserService();

            _currentUser = new User();

            SaveUserCommand = new RelayCommand(this.SaveUser);
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
    }
}
