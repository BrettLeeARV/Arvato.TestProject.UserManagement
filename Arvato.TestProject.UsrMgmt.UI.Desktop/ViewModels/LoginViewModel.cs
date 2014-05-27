using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Threading;
using System.ComponentModel;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class LoginViewModel : PageViewModel
    {
        private IUserService userService;
        private User user;

        public LoginViewModel()
        {
            // set up model data
            userService = new UserService();
            user = new User();
            // for convenience
#if DEBUG
            user.LoginID = "jingtao";
#endif

            // set up commands
            SignInCommand = new RelayCommand<PasswordBox>(this.SignIn);
        }

        public User User
        {
            get
            {
                return user;
            }
            set
            {
                if (user != value)
                {
                    user = value;
                    RaisePropertyChanged("User");
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
        private void SignIn(PasswordBox passwordBox)
        {
            MessengerInstance.Send(new LoadingMessage("Signing in..."));

            Exception loginEx = null;
            bool loggedIn = false;

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (object sender, DoWorkEventArgs e) =>
            {
                // the only way to extract the password out of a PasswordBox is by directly accessing its Password property
                user.Password = passwordBox.Password;
                try
                {
                    loggedIn = userService.Login(user);
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
                    StateManager.CurrentUser = user;
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
    }
}
