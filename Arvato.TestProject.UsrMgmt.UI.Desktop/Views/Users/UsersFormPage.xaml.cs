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
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using System.Diagnostics;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;
using System.ComponentModel;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Views
{
    /// <summary>
    /// Interaction logic for FormPage.xaml
    /// </summary>
    public partial class UsersFormPage : UserControl
    {

        public UsersFormPage()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Because it is not possible to data-bind to the Password property of a PasswordBox,
        /// this approach uses a code-behind event handler to watch for value changes in the
        /// PasswordBox, and assign the new value to the ViewModel
        /// Note: slightly breaks MVVM pattern, but acceptable as a workaround
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void passwordTextBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            ((UsersFormViewModel) DataContext).CurrentUser.Password = passwordTextBox.Password;
        }

    }
}
