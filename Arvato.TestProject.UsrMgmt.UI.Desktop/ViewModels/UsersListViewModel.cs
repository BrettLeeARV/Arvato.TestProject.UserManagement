using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Windows.Input;
using System.Windows.Navigation;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.User;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;

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
            userService = new UserServiceClient();
            FormViewModel = new UsersFormViewModel();

            // set up commands
            AddUserCommand = new RelayCommand(this.AddUser, () => true);
            DeleteUserCommand = new RelayCommand(this.DeleteUser,
                // enable Delete User button if a user is selected
                () => FormViewModel.CurrentUser != null && FormViewModel.CurrentUser.ID != 0);

            MessengerInstance.Register<NotificationMessage>(this, (message) =>
            {
                if (message.Notification == "UserSaved")
                {
                    RefreshUsers();
                    FormViewModel.CurrentUser = new User();
                }
            });
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
                    RaisePropertyChanged("Users");
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
                 MessengerInstance.Send(new LoadingMessage("Deleting user..."));

                Exception exceptionResult = null;
                BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (object sender, DoWorkEventArgs e) =>
                {
                userService.Delete(FormViewModel.CurrentUser);
                };
                worker.RunWorkerCompleted += (object sender, RunWorkerCompletedEventArgs e) =>
                {
                    MessengerInstance.Send(new LoadingMessage(false));
                    RefreshUsers();
                    FormViewModel.CurrentUser= new User();
                };
                worker.RunWorkerAsync();
            }
            catch (Exception)
            {
                // TODO: implement messaging to move MessageBox calls to view code-behind
                MessageBox.Show("There was a problem deleting the user.");
                return;
            }
        }

        #endregion

    }
}
