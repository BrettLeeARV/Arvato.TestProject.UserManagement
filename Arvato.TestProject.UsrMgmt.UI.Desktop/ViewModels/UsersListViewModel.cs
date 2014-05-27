using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersListViewModel : PageViewModel
    {

        private IUserService userService;
        private ICollection<User> users;

        public UsersListViewModel()
            : base()
        {
            // set up model data
            userService = new UserService();
            FormViewModel = new UsersFormViewModel();

            // set up commands
            AddUserCommand = new RelayCommand(this.AddUser, () => true);
            DeleteUserCommand = new RelayCommand(this.DeleteUser,
                // enable Delete User button if a user is selected
                () => FormViewModel.CurrentUser != null);
        }

        public ICollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                if (users != value)
                {
                    users = value;
                    RaisePropertyChanged("Bookings");
                }
            }
        }

        public UsersFormViewModel FormViewModel
        {
            get;
            private set;
        }

        #region Command properties

        public ICommand AddUserCommand
        {
            get;
            private set;
        }
        public ICommand DeleteUserCommand
        {
            get;
            private set;
        }

        #endregion

        protected override void OnNavigatingTo(object s, EventArgs e)
        {
            RefreshUsers();
        }

        private void RefreshUsers()
        {
            Users = userService.GetList();
        }

        #region Command methods

        private void AddUser()
        {
            FormViewModel.CurrentUser = new User();
        }

        private void DeleteUser()
        {
            var result = MessageBox.Show(
                String.Format(@"Are you sure you want to delete user ""{0}""?", FormViewModel.CurrentUser.LoginID),
                "Deleting user",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.No)
            {
                return;
            }

            try
            {
                userService.Delete(FormViewModel.CurrentUser);
            }
            catch (Exception)
            {
                // TODO: implement messaging to move MessageBox calls to view code-behind
                MessageBox.Show("There was a problem deleting the user.");
                return;
            }

            FormViewModel.CurrentUser = null;
            RefreshUsers();
        }

        #endregion

    }
}
