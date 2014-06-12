using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.User;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Utils
{
    [ValueConversion(typeof(object), typeof(string))]
    public class UserInfoConverter : BaseConverter, IValueConverter
    {
        private static IEnumerable<User> _allUser;

        private IUserService userService;

        private void getAllRoom()
        {
            if (PageViewModel.StateManager.AllUser== null)
            {
                userService = new UserServiceClient();
                _allUser = userService.GetList();
                PageViewModel.StateManager.AllUser = _allUser;
            }
            else
                _allUser = PageViewModel.StateManager.AllUser;
        }

        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {

            string type = parameter as string;
            getAllRoom();
            if (type == "FirstName")
            {
                return _allUser.First(r => r.ID == int.Parse(value.ToString())).FirstName;
            }
            else if (type == "LastName")
            {
                return _allUser.First(r => r.ID == int.Parse(value.ToString())).LastName;
            }
            else if (type == "LoginID")
            {
                return _allUser.First(r => r.ID == int.Parse(value.ToString())).LoginID;
            }
            else
            {
                return value.ToString();
            }
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
