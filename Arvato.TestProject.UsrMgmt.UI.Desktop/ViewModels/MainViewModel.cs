using System;
using System.Collections.Generic;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Messages;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using GalaSoft.MvvmLight.Ioc;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private IDictionary<string, ViewModelBase> _viewModels;

        private ViewModelBase _currentViewModel;

        private ViewModelLocator _locator;

        private bool _isLoading;
        private string _loadingText;
        private const string _defaultLoadingText = "Please wait...";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}

            IsLoading = false;
            LoadingText = _defaultLoadingText;

            _locator = (ViewModelLocator) Application.Current.Resources["Locator"];

            // Insert needed ViewModels into dictionary
            _viewModels = new Dictionary<string, ViewModelBase>();
            _viewModels.Add("Login", _locator.Login);
            _viewModels.Add("MainMenu", _locator.MainMenu);

            // Set initial ViewModel
            CurrentViewModel = _viewModels["Login"];

            // Command to change the ViewModel, given a string representing the ViewModel name
            ChangePageCommand = new RelayCommand<Type>(this.ChangeViewModel);

            // Subscribe to ChangeViewModelMessages
            Messenger.Default.Register<ChangePageMessage>
            (
                 this,
                 (action) => ReceiveChangeViewModelMessage(action)
            );

            Messenger.Default.Register<NotificationMessage>
            (
                this,
                (action) => ReceiveNotificationMessage(action)
            );

            Messenger.Default.Register<LoadingMessage>
            (
                this,
                (action) => ReceiveLoadingMessage(action)
            );
        }

        public bool IsLoading
        {
            get
            {
                return _isLoading;
            }
            set
            {
                if (value == _isLoading)
                {
                    return;
                }
                _isLoading = value;
                RaisePropertyChanged("IsLoading");
            }
        }

        public string LoadingText
        {
            get
            {
                return _loadingText;
            }
            set
            {
                if (value == _loadingText)
                {
                    return;
                }
                _loadingText = value;
                RaisePropertyChanged("LoadingText");
            }
        }

        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public RelayCommand<Type> ChangePageCommand
        {
            get;
            private set;
        }

        private void ChangeViewModel(Type type)
        {
            var viewModel = (ViewModelBase) SimpleIoc.Default.GetInstance(type);
            if (viewModel != null)
            {
                CurrentViewModel = viewModel;
            }
            else
            {
                // TODO: exception handling
                throw new Exception("MainViewModel.ChangeViewModel: View model not found");
            }
        }

        private void ReceiveChangeViewModelMessage(ChangePageMessage action)
        {
            ChangeViewModel(action.ViewModel);
        }

        private void ReceiveNotificationMessage(NotificationMessage action)
        {
            if (action.Notification == "LoggedIn")
            {
                LoadPostLoginViewModels();
            }
        }

        private void ReceiveLoadingMessage(LoadingMessage action)
        {
            IsLoading = action.ShowLoading;
            if (!String.IsNullOrEmpty(action.Text))
            {
                LoadingText = action.Text;
            }
            else
            {
                LoadingText = _defaultLoadingText;
            }
        }

        private void LoadPostLoginViewModels()
        {
            _viewModels.Add("UsersList", _locator.UsersList);
            _viewModels.Add("BookingsList", _locator.BookingsList);
            _viewModels.Add("BookingsCreate", _locator.BookingsCreate);
        }

    }
}