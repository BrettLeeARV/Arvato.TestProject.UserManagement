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
    public class RoomAvailabilityConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            if (PageViewModel.StateManager.AllBookedItem == null)
            {
                return "Green";
            }
            else if (PageViewModel.StateManager.AllBookedItem.Contains("R-" + value.ToString()))
            {
                return "Red";
            }
            else
            {
                return "Green";
            }
            
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
