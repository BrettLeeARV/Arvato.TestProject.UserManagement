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
            public string ViewModelName
            {
                get;
                set;
            }
        }
        
        public MainMenuViewModel()
        {
            MenuItems = new List<MainMenuItem>()
            {
                new MainMenuItem() { Title = "Manage Users" , ViewModelName = "UsersList" },
                new MainMenuItem() { Title = "Manage Bookings" , ViewModelName = "BookingsCreate" }
            };
            
            NavigateToCommand = new RelayCommand<string>(this.NavigateTo);
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

        /// <summary>
        /// This slightly breaks the MMV
        /// </summary>
        /// <param name="passwordBox">The PasswordBox control to read from.</param>
        private void NavigateTo(string viewModelName)
        {
            var msg = new ChangeViewModelMessage(viewModelName);
            Messenger.Default.Send<ChangeViewModelMessage>(msg);
        }

        #endregion
    }
}
