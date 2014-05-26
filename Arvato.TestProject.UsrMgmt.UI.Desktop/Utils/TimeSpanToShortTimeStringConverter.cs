using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Utils
{
    [ValueConversion(typeof(object), typeof(string))]
    public class TimeSpanToShortTimeStringConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            if (value is string)
            {
                return value;
            }
            if (value is TimeSpan)
            {
                var timespan = (TimeSpan)value;
                return new DateTime(0).Add(timespan).ToShortTimeString();
            }
            return DependencyProperty.UnsetValue;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            if (value is TimeSpan)
            {
                return value;
            }
            DateTime time;
            if ( DateTime.TryParse((string) value, out time))
            {
                return new TimeSpan(time.Hour, time.Minute, time.Second);
            }
            return DependencyProperty.UnsetValue;
        }

    }

}
