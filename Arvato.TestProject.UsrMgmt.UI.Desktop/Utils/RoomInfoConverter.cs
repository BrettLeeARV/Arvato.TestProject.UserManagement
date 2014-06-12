using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.UI.Desktop.Services.Room;
using Arvato.TestProject.UsrMgmt.UI.Desktop.ViewModels;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Utils
{
    [ValueConversion(typeof(object), typeof(string))]
    public class RoomInfoConverter : BaseConverter, IValueConverter
    {
        private static IEnumerable<Room> _allRoom;

        private IRoomService roomService;

        private void getAllRoom()
        {
            if (PageViewModel.StateManager.AllRoom == null)
            {
                roomService = new RoomServiceClient();
                _allRoom = roomService.GetList(false);
                PageViewModel.StateManager.AllRoom = _allRoom;
            }
            else
                _allRoom = PageViewModel.StateManager.AllRoom;
        }

        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {

            string type = parameter as string;
            getAllRoom();
            if (type == "RoomName")
            {
                string name= _allRoom.First(r => r.ID == int.Parse(value.ToString())).Name;
                return name;
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
