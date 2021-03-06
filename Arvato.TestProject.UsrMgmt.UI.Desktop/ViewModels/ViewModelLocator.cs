/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Arvato.TestProject.UsrMgmt.UI.Desktop"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}

            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<LoginViewModel>();
            SimpleIoc.Default.Register<MainMenuViewModel>();
            SimpleIoc.Default.Register<UsersListViewModel>();
            SimpleIoc.Default.Register<UsersFormViewModel>();
            SimpleIoc.Default.Register<BookingsListViewModel>();
            SimpleIoc.Default.Register<RoomsListViewModel>();
            SimpleIoc.Default.Register<RoomsFormViewModel>();
            SimpleIoc.Default.Register<AssetsListViewModel>();
            SimpleIoc.Default.Register<AssetsFormViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public LoginViewModel Login
        {
            get
            {
                return ServiceLocator.Current.GetInstance<LoginViewModel>();
            }
        }

        public MainMenuViewModel MainMenu
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainMenuViewModel>();
            }
        }

        public UsersListViewModel UsersList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UsersListViewModel>();
            }
        }

        public UsersFormViewModel UsersForm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<UsersFormViewModel>();
            }
        }

        public BookingsListViewModel BookingsList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BookingsListViewModel>();
            }
        }

        public BookingsFormViewModel BookingsCreate
        {
            get
            {
                return ServiceLocator.Current.GetInstance<BookingsFormViewModel>();
            }
        }

        public RoomsListViewModel RoomsList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RoomsListViewModel>();
            }
        }

        public RoomsFormViewModel RoomsForm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<RoomsFormViewModel>();
            }
        }

        public AssetsListViewModel AssetsList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AssetsListViewModel>();
            }
        }

        public AssetsFormViewModel AssetsForm
        {
            get
            {
                return ServiceLocator.Current.GetInstance<AssetsFormViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}