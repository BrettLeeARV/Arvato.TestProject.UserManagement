﻿using System;
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
            
        }

        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            if (usernameTextBox.Text == "user" && passwordTextBox.Password == "password")
            {
                NavigationService.Navigated += delegate(object nsender, NavigationEventArgs ne)
                {
                    var frame = (Frame)nsender;
                    frame.NavigationService.RemoveBackEntry();
                };
                NavigationService.Navigate(new Uri("Pages/MainMenuPage.xaml", UriKind.Relative));
            }
        }
    }
}