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
using System.Collections.ObjectModel;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersFormViewModel : PageViewModel, IDataErrorInfo
    {
        private IUserService _userService;
        private IRoleComponent _roleComponent;
        private User _currentUser;

        private Role _filterRole;
        private ObservableCollection<RoleComboBoxItem> _allRoleOptions;

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
                _roleComponent = new RoleComponent();

                BindRoleComboBox();

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

                    if (_currentUser != null)
                    {
                        if (_currentUser.RoleID == 0)
                        {
                            FilterRole = null;
                        }
                        else
                        {
                            FilterRole = RoleOptions.First(r => r.Role != null && r.Role.ID == _currentUser.RoleID).Role;
                        }
                    }

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
                if (_currentUser == null)
                {
                    return null;
                }
                else
                {
                    return _currentUser.FirstName;
                }
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
                if (_currentUser == null)
                {
                    return null;
                }
                else
                {
                    return _currentUser.LastName;
                }
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
                if (_currentUser == null)
                {
                    return null;
                }
                else
                {
                    return _currentUser.LoginID;
                }
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
                if (_currentUser == null)
                {
                    return null;
                }
                else
                {
                    return _currentUser.Email;
                }
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
                if (_currentUser == null)
                {
                    return false;
                }
                else
                {
                    return _currentUser.IsWindowAuthenticate;
                }
            }
            set
            {
                if (value != _currentUser.IsWindowAuthenticate)
                {
                    _currentUser.IsWindowAuthenticate = value;

                    RaisePropertyChanged("LoginID");
                    RaisePropertyChanged("IsWindowAuthenticate");
                }
            }
        }

        public string Password
        {
            get
            {
                // Always return empty string
                //return string.Empty;
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

        public class RoleComboBoxItem
        {
            public Role Role { get; set; }
            public string DisplayString
            {
                get
                {
                    if (Role == null)
                        return null;
                    else
                        return Role.RoleName;
                }
            }
        }

        public ObservableCollection<RoleComboBoxItem> RoleOptions
        {
            get
            {
                return _allRoleOptions;
            }
            private set
            {
                if (value == _allRoleOptions)
                {
                    return;
                }
                _allRoleOptions = value;
                RaisePropertyChanged("RoleOptions");
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
            if (IsValid)
            {
                // Massage the User object before sending it off
                if (_currentUser.IsWindowAuthenticate)
                {
                    Password = null;
                }

                MessengerInstance.Send(new LoadingMessage("Saving user..."));

                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                    _currentUser.RoleID = FilterRole.ID;
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

        public Role FilterRole
        {
            get
            {
                return _filterRole;
            }
            set
            {
                if (value == _filterRole)
                {
                    return;
                }
                _filterRole = value;
                RaisePropertyChanged("FilterRole");
            }
        }

        #endregion

        #region FluentValidation Members

        public bool IsValid
        {
            get { return SelfValidate().IsValid; }
        }

        public ValidationResult SelfValidate()
        {
            ValidationResult validateUser = new ValidationResult();
            if (CurrentUser != null)
            {
                validateUser = ValidationHelper.Validate<UserValidator, User>(CurrentUser);
            }
            var validateVM = ValidationHelper.Validate<UsersFormValidator, UsersFormViewModel>(this);
            return new ValidationResult(validateUser.Errors.Concat(validateVM.Errors));
        }

        #endregion

        #region IDataErrorInfo Members

        public string Error
        {
            get
            {
                // Error property can be used to tell what is wrong with the View Model, in general (e.g. "You've got a clashhhh...")
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
                
                string error = __ColumnResults != null ? __ColumnResults.ErrorMessage : string.Empty;
                return error;
            }
        }

        #endregion

        #region Binding Methods
        private void BindRoleComboBox()
        {
            var roles = _roleComponent.GetList();
            _allRoleOptions = new ObservableCollection<RoleComboBoxItem>()
                {
                    new RoleComboBoxItem() { Role = null }
                };
            foreach (var role in roles)
            {
                _allRoleOptions.Add(new RoleComboBoxItem() { Role = role });
            }
            RaisePropertyChanged("RoleOptions");
        }
        #endregion
    }
}
