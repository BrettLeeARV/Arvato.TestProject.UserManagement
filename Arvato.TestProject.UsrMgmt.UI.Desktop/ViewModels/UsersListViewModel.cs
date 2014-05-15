using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.MVVM;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class UsersListViewModel : ViewModelBase
    {

        private IUserService userService;
        private ICollection<User> users;

        public UsersListViewModel()
        {
            userService = new UserService();
            users = userService.GetList();
        }

        public ICollection<User> Users
        {
            get
            {
                return users;
            }
            set
            {
                if (users != value)
                {
                    users = value;
                    OnPropertyChanged("Users");
                }
            }
        }
    }
}
