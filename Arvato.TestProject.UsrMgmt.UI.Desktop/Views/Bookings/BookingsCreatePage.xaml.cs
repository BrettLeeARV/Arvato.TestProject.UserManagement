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
using System.Diagnostics;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Views
{
    /// <summary>
    /// Interaction logic for BookingsCreatePage.xaml
    /// </summary>
    public partial class BookingsCreatePage : UserControl
    {
        public BookingsCreatePage()
        {
            InitializeComponent();
        }

        private void comboBox2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var box = (ComboBox)sender;
            Debug.WriteLine(box.Items);
        }
    }
}
