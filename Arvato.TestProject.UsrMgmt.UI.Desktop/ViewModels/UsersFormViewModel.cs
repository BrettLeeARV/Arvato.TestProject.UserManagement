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
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight.Messaging;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersFormViewModel : PageViewModel, IDataErrorInfo
    {
        private IUserService _userService;
        private User _currentUser;

        #region Constructor

        public UsersFormViewModel()
            : base()
        {
            _currentUser = new User();

            if (IsInDesignMode)
            {
                FirstName = "John";
                LastName = "";
                LoginID = "johndoe";
                Email = "john@email.com";
                IsWindowAuthenticate = true;
            }
            else
            {
                _userService = new UserServiceClient();

                SaveUserCommand = new RelayCommand(this.SaveUser,
                    () => IsValid);
            }
        }

        #endregion

        #region Properties

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
                    RaisePropertyChanged("FirstName");
                    RaisePropertyChanged("LastName");
                    RaisePropertyChanged("LoginID");
                    RaisePropertyChanged("Email");
                    RaisePropertyChanged("IsWindowAuthenticate");
                    RaisePropertyChanged("Password");
                }
            }
        }

        public string FirstName
        {
            get
            {
                return _currentUser.FirstName;
            }
            set
            {
                if (value != _currentUser.FirstName)
                {
                    _currentUser.FirstName = value;
                    RaisePropertyChanged("FirstName");
                }
            }
        }

        public string LastName
        {
            get
            {
                return _currentUser.LastName;
            }
            set
            {
                if (value != _currentUser.LastName)
                {
                    _currentUser.LastName = value;
                    RaisePropertyChanged("LastName");
                }
            }
        }

        public string LoginID
        {
            get
            {
                return _currentUser.LoginID;
            }
            set
            {
                if (value != _currentUser.LoginID)
                {
                    _currentUser.LoginID = value;
                    RaisePropertyChanged("LoginID");
                }
            }
        }

        public string Email
        {
            get
            {
                return _currentUser.Email;
            }
            set
            {
                if (value != _currentUser.Email)
                {
                    _currentUser.Email = value;
                    RaisePropertyChanged("Email");
                }
            }
        }

        public bool IsWindowAuthenticate
        {
            get
            {
                return _currentUser.IsWindowAuthenticate;
            }
            set
            {
                if (value != _currentUser.IsWindowAuthenticate)
                {
                    _currentUser.IsWindowAuthenticate = value;
                    RaisePropertyChanged("IsWindowAuthenticate");
                }
            }
        }

        public string Password
        {
            get
            {
                return _currentUser.Password;
            }
            set
            {
                if (value != _currentUser.Password)
                {
                    _currentUser.Password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        #endregion

        #region Command properties

        public ICommand SaveUserCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command methods

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
                MessengerInstance.Send(new LoadingMessage("Saving user..."));

                Exception exceptionResult = null;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    _currentUser = _userService.Save(_currentUser);

                };
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    RaisePropertyChanged("CurrentUser");
                    MessengerInstance.Send(new NotificationMessage("UserSaved"));

                    MessengerInstance.Send(new LoadingMessage(false));
                    Cancel();
                };
                worker.RunWorkerAsync();
            }
        }

        private void Cancel()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(UsersListViewModel)));
        }

        #endregion

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
            get
            {
                // return (_currentUser as IDataErrorInfo).Error;
                throw new NotImplementedException();
            }
        }

        public string this[string columnName]
        {
            get
            {
                StringBuilder error = new StringBuilder();
                if (_currentUser != null)
                {
                    error.Append((_currentUser as IDataErrorInfo)[columnName]);
                    CommandManager.InvalidateRequerySuggested();
                }

                var __ValidationResults = SelfValidate();
                if (__ValidationResults == null) return string.Empty;
                var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
                error.Append(__ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty);

                return error.ToString();
            }
        }

        //public string Error
        //{
        //    get { 
        //        var r = ValidationHelper.GetError(SelfValidate());
        //        return r;
        //    }
        //}

        //public string this[string columnName]
        //{
        //    get
        //    {
        //        var __ValidationResults = SelfValidate();
        //        if (__ValidationResults == null) return string.Empty;
        //        var __ColumnResults = __ValidationResults.Errors.FirstOrDefault<ValidationFailure>(x => string.Compare(x.PropertyName, columnName, true) == 0);
        //        return __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
        //    }
        //}
        #endregion
    }
}
