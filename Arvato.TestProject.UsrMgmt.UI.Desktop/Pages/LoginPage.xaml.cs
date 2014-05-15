using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Diagnostics;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Page
    {
        public LoginPage()
        {
            InitializeComponent();
            Loaded += new RoutedEventHandler(LoginPage_Loaded);
        }

        void LoginPage_Loaded(object sender, RoutedEventArgs e)
        {
            // clear all previous navigation history when brought back to login page
            var frame = NavigationService;
            if (!frame.CanGoBack && !frame.CanGoForward)
            {
                return;
            }

            var entry = frame.RemoveBackEntry();
            while (entry != null)
            {
                entry = frame.RemoveBackEntry();
            }
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            IUserService userService = new UserService();
            User user = new User();
            user.LoginID = loginIDTextBox.Text;
            user.Password = passwordTextBox.Password;
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
                NavigationService.Navigate(new Uri("Pages/UsersListPage.xaml", UriKind.Relative));
            }
            else
            {
                MessageBox.Show("Invalid login ID or password", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
