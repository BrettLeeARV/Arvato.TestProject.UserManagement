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
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using System.Diagnostics;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Pages.Users
{
    /// <summary>
    /// Interaction logic for FormPage.xaml
    /// </summary>
    public partial class FormPage : Page
    {
        User currentUser;
        IUserService userService = new UserService();

        // new user
        public FormPage() : this(new User())
        {

        }

        // edit user
        public FormPage(User user)
        {
            InitializeComponent();

            currentUser = user;
            this.DataContext = currentUser;

            Debug.WriteLine("FormPage.xaml: " + currentUser);
        }

        private void saveButton_Click(object sender, RoutedEventArgs e)
        {
            bool success = false;
            try
            {
                userService.Save(currentUser);
                success = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Warning);
            }

            if (success)
            {
                NavigationService.GoBack();
            }
        }

        private void cancelButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }

    }
}