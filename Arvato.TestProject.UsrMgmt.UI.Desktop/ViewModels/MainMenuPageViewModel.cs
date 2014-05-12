using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels
{
    public class MainMenuPageViewModel
    {

        private IUserService _userService;

        public MainMenuPageViewModel()
        {
            _userService = new UserService();
        }

        public ICollection<User> Users
        {
            get
            {
                return _userService.GetList();
            }
        }

    }
}
