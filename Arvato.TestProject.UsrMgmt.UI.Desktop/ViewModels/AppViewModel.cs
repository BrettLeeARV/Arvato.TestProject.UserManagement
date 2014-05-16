using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.UI.Desktop.MVVM;
using System.Windows.Input;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class AppViewModel : ViewModelBase
    {
        #region Fields

        private ICommand _changePageCommand;

        private ViewModelBase _currentPageViewModel;
        private List<ViewModelBase> _pageViewModels;

        #endregion

        public AppViewModel()
        {
            // Add available pages
            PageViewModels.Add(new LoginViewModel());
            PageViewModels.Add(new UsersListViewModel());

            _changePageCommand = new ActionCommand(
                       this.ChangeViewModel,
                        () => true);

            // Set starting page
            CurrentPageViewModel = PageViewModels[0];
        }

        #region Properties / Commands

        public ICommand ChangePageCommand
        {
            get
            {
                return _changePageCommand;
            }
        }

        public List<ViewModelBase> PageViewModels
        {
            get
            {
                if (_pageViewModels == null)
                    _pageViewModels = new List<ViewModelBase>();

                return _pageViewModels;
            }
        }

        public ViewModelBase CurrentPageViewModel
        {
            get
            {
                return _currentPageViewModel;
            }
            set
            {
                if (_currentPageViewModel != value)
                {
                    _currentPageViewModel = value;
                    OnPropertyChanged("CurrentPageViewModel");
                }
            }
        }

        #endregion

        #region Methods

        private void ChangeViewModel(object _viewmodel)
        {
            var viewModel = (ViewModelBase) _viewmodel;
            if (!PageViewModels.Contains(viewModel))
                PageViewModels.Add(viewModel);

            CurrentPageViewModel = PageViewModels
                .FirstOrDefault(vm => vm == viewModel);
        }

        #endregion
    }
}
