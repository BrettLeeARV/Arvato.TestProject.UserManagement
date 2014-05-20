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
using System.Windows.Controls;
using System.Windows;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class LoginViewModel : ViewModelBase
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
            // the only way to extract the password out of a PasswordBox is by directly accessing its Password property
            user.Password = passwordBox.Password;
            try
            {
                userService.Login(user);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (user.ID > 0)
            {
                //NavigationService.Navigate(new Uri("Pages/UsersListPage.xaml", UriKind.Relative));
                MessageBox.Show("Logged in!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {
                MessageBox.Show("Invalid login ID or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        #endregion
    }
}
