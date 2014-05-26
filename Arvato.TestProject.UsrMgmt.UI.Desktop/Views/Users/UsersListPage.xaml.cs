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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Views
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class UsersListPage : UserControl
    {

        User selectedUser = null;

        public UsersListPage()
        {
            InitializeComponent();
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
                    //NavigationService.Refresh();
                }
            }
        }
    }
}
