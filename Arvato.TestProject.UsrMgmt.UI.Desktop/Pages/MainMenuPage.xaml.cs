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
    }
}
