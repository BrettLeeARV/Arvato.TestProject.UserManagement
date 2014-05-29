using System.Windows.Input;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;
using System;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public abstract class PageViewModel : ViewModelBase
    {
        private ICommand _mainMenuCommand = null;
        private bool _isCurrentlyShown;

        /// <summary>
        /// Hooks up the convenience navigation event handlers
        /// </summary>
        public PageViewModel()
        {
            // Provide two event handlers as convenience for subclasses to override or extend
            this.NavigatingTo += OnNavigatingTo;
            this.NavigatingFrom += OnNavigatingFrom;
        }

        public bool IsCurrentlyShown
        {
            get
            {
                return _isCurrentlyShown;
            }
            set
            {
                if (value == _isCurrentlyShown)
                {
                    return;
                }
                _isCurrentlyShown = value;
                EventArgs ea = new EventArgs();
                if (_isCurrentlyShown)
                {
                    RaiseNavigatingTo(ea);
                }
                else
                {
                    RaiseNavigatingFrom(ea);
                }
                RaisePropertyChanged("IsCurrentlyShown");
            }
        }

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

        public event EventHandler NavigatingTo;
        public event EventHandler NavigatingFrom;

        protected void RaiseNavigatingTo(EventArgs e)
        {
            Debug.WriteLine(this.GetType() + ".OnNavigatingTo");

            EventHandler handler = NavigatingTo;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected void RaiseNavigatingFrom(EventArgs e)
        {
            Debug.WriteLine(this.GetType() + ".OnNavigatingFrom");

            EventHandler handler = NavigatingFrom;
            if (handler != null)
            {
                handler(this, e);
            }
        }

        protected virtual void OnNavigatingTo(object s, EventArgs e)
        {
            // Possible ideas include refreshing view model data, opening resources...
        }

        protected virtual void OnNavigatingFrom(object s, EventArgs e)
        {
            // Possible ideas include saving view model data, closing resources...
        }

        private void GoToMainMenu()
        {
            MessengerInstance.Send(new ChangePageMessage(typeof(MainMenuViewModel)));
        }
    }
}
