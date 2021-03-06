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
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using System.Diagnostics;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using System.ComponentModel;
using System.Threading;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Views
{
    public partial class UsersFormPage : UserControl
    {
        public delegate void SimpleDelegate();

        public UsersFormPage()
        {
            InitializeComponent();
        }

        #region highlightPasswordBox
        private void passwordTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(passwordTextBox.Password))
            {
                Thread t = new Thread(new ThreadStart(this.SelectPassword));
                t.Start();
            }
        }

        private void SelectPassword()
        {
            Thread.Sleep(10);
            passwordTextBox.Dispatcher.BeginInvoke(System.Windows.Threading.DispatcherPriority.Normal, new SimpleDelegate(delegate() { passwordTextBox.SelectAll(); }));
        }
        #endregion
    }
}
