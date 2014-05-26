using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public abstract class PageViewModel : ViewModelBase
    {
        private ICommand _mainMenuCommand = null;

        public static StateManager StateManager
        {
            get
            {
                return StateManager.Instance;
            }
        }

        public ICommand MainMenuCommand
        {
            get
            {
                if (_mainMenuCommand == null)
                {
                    _mainMenuCommand = new RelayCommand(this.GoToMainMenu);
                }
                return _mainMenuCommand;
            }
        }

        private void GoToMainMenu()
        {
            MessengerInstance.Send(new ChangeViewModelMessage("MainMenu"));
        }
    }
}
