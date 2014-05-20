using System;
using System.Collections.Generic;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Diagnostics;

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

            ViewModelLocator vml = new ViewModelLocator();

            // Insert needed ViewModels into dictionary
            _viewModels = new Dictionary<string, ViewModelBase>();
            _viewModels.Add("Login", vml.Login);
            _viewModels.Add("UsersList", vml.UsersList);

            // Set initial ViewModel
            CurrentViewModel = _viewModels["Login"];

            // Command to change the ViewModel, given a string representing the ViewModel name
            ChangeViewModelCommand = new RelayCommand<string>(this.ChangeViewModel);
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

        public RelayCommand<string> ChangeViewModelCommand
        {
            get;
            private set;
        }

        private void ChangeViewModel(string name)
        {
            Debug.WriteLine("ChangeViewModel called");
            if (_viewModels.ContainsKey(name))
            {
                CurrentViewModel = _viewModels[name];
                Debug.WriteLine(_viewModels[name]);
                Debug.WriteLine("MainViewModel.ChangeViewModel: View model is now = " + name);
            }
            else
            {
                throw new Exception("MainViewModel.ChangeViewModel: View model not found");
            }
        }

    }
}