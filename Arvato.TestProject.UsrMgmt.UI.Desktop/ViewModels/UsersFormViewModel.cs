using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GalaSoft.MvvmLight;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersFormViewModel : ViewModelBase
    {
        private User _currentUser;

        public UsersFormViewModel()
        {

        }

        public User CurrentUser
        {
            get
            {
                return _currentUser;
            }
            set
            {
                if (value != _currentUser)
                {
                    _currentUser = value;
                    RaisePropertyChanged("CurrentUser");
                }
            }
        }
    }
}
