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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {

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
            NavigationService.Navigated += delegate(object nsender, NavigationEventArgs ne)
            {
                var frame = (Frame)nsender;
                if (!frame.CanGoBack && !frame.CanGoForward)
                {
                    return;
                }

                var entry = frame.RemoveBackEntry();
                while (entry != null)
                {
                    entry = frame.RemoveBackEntry();
                }
                Console.WriteLine("cleared all back entries");
            };
            NavigationService.Navigate(new Uri("Pages/LoginPage.xaml", UriKind.Relative));
        }

        private void addUserButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void dataGrid1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DataGrid dataGrid = (DataGrid)sender;
            User selected = (User)dataGrid.SelectedItem;
            if (selected != null)
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
    }
}
