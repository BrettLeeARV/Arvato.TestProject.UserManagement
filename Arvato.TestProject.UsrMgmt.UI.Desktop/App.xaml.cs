using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            AppDomain.CurrentDomain.UnhandledException += new
               UnhandledExceptionEventHandler(this.AppDomainUnhandledExceptionHandler);

            base.OnStartup(e);
        }
        
        void AppDomainUnhandledExceptionHandler(object sender, UnhandledExceptionEventArgs ea)
        {
            MessageBox.Show(
                String.Format(@"An unhandled exception has occured:
{0}

The application will now close.", ((Exception)ea.ExceptionObject).Message),
                "Fatal error",
                MessageBoxButton.OK,
                MessageBoxImage.Error);

            // Exit app because when the exception has reached the "point of no return"
            Application.Current.Shutdown();
        }

    }
}
