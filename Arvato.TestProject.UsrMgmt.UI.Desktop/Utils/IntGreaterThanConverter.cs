using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using System.Diagnostics;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Utils
{
    [ValueConversion(typeof(object), typeof(string))]
    public class IntGreaterThanConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            //Debugger.Break();
            int val = System.Convert.ToInt32(value);
            int param = System.Convert.ToInt32(parameter);
            return val > param;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
