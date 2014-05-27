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
        private PageViewModel _currentViewModel;

        private bool _isLoading;
        private string _loadingText;
        private const string _defaultLoadingText = "Please wait...";

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            IsLoading = false;
            LoadingText = _defaultLoadingText;

            // Set initial ViewModel
            ChangeViewModelByType(typeof(LoginViewModel));

            // Command to change the ViewModel, given a string representing the ViewModel name
            ChangePageCommand = new RelayCommand<Type>(this.ChangeViewModelByType);

            // Subscribe to ChangePageMessage (change view model)
            Messenger.Default.Register<ChangePageMessage>
            (
                 this,
                 (action) => ReceiveChangePageMessage(action)
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

        public PageViewModel CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                {
                    return;
                }
                if (_currentViewModel != null)
                {
                    _currentViewModel.IsCurrentlyShown = false;
                }
                _currentViewModel = value;
                _currentViewModel.IsCurrentlyShown = true;
                RaisePropertyChanged("CurrentViewModel");
            }
        }

        public RelayCommand<Type> ChangePageCommand
        {
            get;
            private set;
        }

        private void ReceiveChangePageMessage(ChangePageMessage action)
        {
            if (action.ChangeBy == ChangePageMessage.MessageType.Type)
            {
                ChangeViewModelByType(action.ViewModelType);
            }
            else
            {
                ChangeViewModelByInstance(action.ViewModelInstance);
            }
        }

        private void ChangeViewModelByType(Type type)
        {
            var viewModel = (PageViewModel) SimpleIoc.Default.GetInstance(type);
            if (viewModel != null)
            {
                CurrentViewModel = viewModel;
            }
            else
            {
                // TODO: exception handling
                throw new Exception("MainViewModel.ChangeViewModelByType: View model not found");
            }
        }

        private void ChangeViewModelByInstance(PageViewModel instance)
        {
            var viewModel = instance;
            if (viewModel != null)
            {
                CurrentViewModel = viewModel;
            }
            else
            {
                // TODO: exception handling
                throw new Exception("MainViewModel.ChangeViewModelByInstance: View model not found");
            }
        }

        private void ReceiveNotificationMessage(NotificationMessage action)
        {
            if (action.Notification == "LoggedIn")
            {
                // do nothing for now
            }
        }

        private void ReceiveLoadingMessage(LoadingMessage action)
        {
            IsLoading = action.ShowLoading;
            if (String.IsNullOrEmpty(action.Text))
            {
                LoadingText = _defaultLoadingText;
            }
            else
            {
                LoadingText = action.Text;
            }
        }

    }
}