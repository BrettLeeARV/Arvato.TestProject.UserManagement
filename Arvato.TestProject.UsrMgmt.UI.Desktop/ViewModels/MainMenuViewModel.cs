using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class MainMenuViewModel : PageViewModel
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
            : base()
        {
            MenuItems = GetMenuItemByRoleID(StateManager.CurrentUser.RoleID);

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

        #region Binding Methods
        private List<MainMenuItem> GetMenuItemByRoleID(int RoleID)
        {
            List<MainMenuItem> displayMenuItem = new List<MainMenuItem>();
            IRoleComponent role = new RoleComponent();
            List<ModuleControl> menuList = role.GetMenuItemsByRoleID(RoleID);

            foreach (ModuleControl module in menuList)
            {
                displayMenuItem.Add(new MainMenuItem() { Title = module.Title, ViewModel = Type.GetType(module.ModuleName) });
            }

            return displayMenuItem;
        }
        #endregion
    }
}
