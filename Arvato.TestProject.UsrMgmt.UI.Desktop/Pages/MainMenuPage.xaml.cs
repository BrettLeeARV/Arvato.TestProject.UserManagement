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
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {

        User selectedUser = null;

        private MainMenuPageViewModel _viewModel;
        public MainMenuPageViewModel ViewModel
        {
            get
            {
                if (_viewModel == null)
                {
                    _viewModel = new MainMenuPageViewModel();
                }
                return _viewModel;
            }
        }

        public MainMenuPage()
        {
            InitializeComponent();

            this.DataContext = ViewModel;

            editUserButton.IsEnabled = false;
            deleteUserButton.IsEnabled = false;
        }

        private void signOutButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            selectedUser = (User)dataGrid.SelectedItem;
            if (selectedUser != null)
            {
                editUserButton.IsEnabled = true;
                deleteUserButton.IsEnabled = true;
            }
            else
            {
                editUserButton.IsEnabled = false;
                deleteUserButton.IsEnabled = false;
            }
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Add User screen coming soon...");
            NavigationService.Navigate(new Pages.Users.FormPage());
        }

        private void editUserButton_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show("Edit User screen coming soon... for user ID " + selectedUser.ID);
            NavigationService.Navigate(new Pages.Users.FormPage(selectedUser));
        }

        private void deleteUserButton_Click(object sender, RoutedEventArgs e)
        {
            var message = String.Format("Are you sure you want to delete the user \"{0}\"?", selectedUser.LoginID);
            var result = MessageBox.Show(message, "Deleting user", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            
            if (result == MessageBoxResult.Yes)
            {
                var success = false;
                try
                {
                    IUserService userService = new UserService();
                    userService.Delete(selectedUser);
                    success = true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Couldn't delete user", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                if (success)
                {
                    NavigationService.Refresh();
                }
            }
        }
    }
}
