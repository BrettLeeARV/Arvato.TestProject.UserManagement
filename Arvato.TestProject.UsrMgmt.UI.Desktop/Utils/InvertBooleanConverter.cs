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
    public class InvertBooleanConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            bool val = System.Convert.ToBoolean(value);
            return !val;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }

    }

}
