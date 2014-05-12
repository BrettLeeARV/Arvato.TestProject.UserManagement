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

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    /// <summary>
    /// Interaction logic for MainMenuPage.xaml
    /// </summary>
    public partial class MainMenuPage : Page
    {
        public ICollection<object> DesignData
        {
            get
            {
                return new List<object>()
                {
                    new { Room = "Room A", StartTime = "09.00", EndTime = "10.00", BookedBy = "JT" },
                    new { Room = "Room A", StartTime = "10.00", EndTime = "11.00", BookedBy = "SS" },
                    new { Room = "Room B", StartTime = "09.00", EndTime = "10.00", BookedBy = "WL" }
                };
            }
        }


        public MainMenuPage()
        {
            InitializeComponent();

            this.DataContext = DesignData;
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
