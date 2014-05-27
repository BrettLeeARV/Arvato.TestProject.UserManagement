using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class MainMenuViewModel : ViewModelBase
    {
        public class MainMenuItem
        {
            public string Title
            {
                get;
                set;
            }
            public Type ViewModel
            {
                get;
                set;
            }
        }
        
        public MainMenuViewModel()
        {
            MenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Manage Users" , ViewModel = typeof(UsersListViewModel) },
                new MainMenuItem() { Title = "Manage Bookings" , ViewModel = typeof(BookingsListViewModel) }
            };
            
            NavigateToCommand = new RelayCommand<Type>(this.NavigateTo);
        }

        public IList<MainMenuItem> MenuItems
        {
            get;
            private set;
        }

        #region Command properties

        public ICommand NavigateToCommand
        {
            get;
            private set;
        }

        #endregion

        #region Command methods

        private void NavigateTo(Type viewModel)
        {
            var msg = new ChangePageMessage(viewModel);
            Messenger.Default.Send<ChangePageMessage>(msg);
        }

        #endregion
    }
}
