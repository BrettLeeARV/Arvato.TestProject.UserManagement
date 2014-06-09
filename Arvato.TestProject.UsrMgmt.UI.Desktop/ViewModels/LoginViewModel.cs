using System;
using System.ComponentModel;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.Entity.Validator;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using FluentValidation.Results;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Diagnostics;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class LoginViewModel : PageViewModel, IDataErrorInfo
    {
        private IUserComponent _userService;
        private User _user;
        private string _loginID;
        private string _password;

        public LoginViewModel()
            : base()
        {
            // set up model data
            _userService = new UserComponent();
            _user = new User();
            // for convenience
#if DEBUG
            _loginID = "jingtao";
            _password = "password";
#endif

            // set up commands
            SignInCommand = new RelayCommand(this.SignIn, () => IsValid);
        }

        public string LoginID
        {
            get
            {
                return _loginID;
            }
            set
            {
                if (_loginID != value)
                {
                    _loginID = value;
                    RaisePropertyChanged("LoginID");
                }
            }
        }

        public string Password
        {
            get
            {
                return _password;
            }
            set
            {
                if (_password != value)
                {
                    _password = value;
                    RaisePropertyChanged("Password");
                }
            }
        }

        #region Command properties

        public ICommand SignInCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command methods

        /// <summary>
        /// This slightly breaks the MMV
        /// </summary>
        /// <param name="passwordBox">The PasswordBox control to read from.</param>
        private void SignIn()
        {
            MessengerInstance.Send(new LoadingMessage("Signing in..."));

            _user.LoginID = _loginID;
            _user.Password = _password;

            Exception loginEx = null;
            bool loggedIn = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                try
                {
                    loggedIn = _userService.Login(_user);
                }
                catch (Exception ex)
                {
                    loginEx = ex;
                }
            };
            worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
            {
                if (loggedIn)
                {
                    StateManager.CurrentUser = _user;
                    PostLogIn();
                }
                else
                {
                    if (loginEx == null)
                    {
                        MessageBox.Show("Invalid login ID or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    else
                    {
                        MessageBox.Show(loginEx.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                MessengerInstance.Send(new LoadingMessage(false));
            };
            worker.RunWorkerAsync();
        }

        #endregion

        private void PostLogIn()
        {
            var loggedInMessage = new NotificationMessage("LoggedIn");
            MessengerInstance.Send<NotificationMessage>(loggedInMessage);

            var msg = new ChangePageMessage(typeof(MainMenuViewModel));
            MessengerInstance.Send<ChangePageMessage>(msg);
        }

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public FluentValidation.Results.ValidationResult SelfValidate()
        {
            var r = ValidationHelper.Validate<LoginFormValidator, LoginViewModel>(this);
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
