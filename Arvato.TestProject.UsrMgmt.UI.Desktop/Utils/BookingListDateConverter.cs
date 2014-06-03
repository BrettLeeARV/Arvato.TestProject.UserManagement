using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.UI.Desktop.Utils
{
    [ValueConversion(typeof(object), typeof(string))]
    public class BookingListDateConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            string type = parameter as string;

            if (type == "Duration")
            {
                Booking booking = (Booking)value;
                TimeSpan ts = booking.EndDate - booking.StartDate;
                return String.Format("({0})", ToFriendlyDuration(ts));
            }
            else
            {
                DateTime date = (DateTime)value;
                switch (type)
                {
                    case "Day":
                        return date.ToString("dd"); ;
                    case "MonthYear":
                        return date.ToString("MMM yy").ToUpper();
                    case "Time":
                        return date.ToShortTimeString();
                }
            }
            throw new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
                        System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        private string ToFriendlyDuration(TimeSpan ts)
        {
            int days = ts.Days;
            int hours = ts.Hours;
            int minutes = ts.Minutes;

            StringBuilder sb = new StringBuilder();
            if (days > 0)
            {
                sb.Append(days.ToString() + " days");
            }
            if (hours > 0)
            {
                sb.Append(hours.ToString() + " hours");
            }
            if (minutes > 0)
            {
                sb.Append(minutes.ToString() + " minutes");
            }
            return sb.ToString();
        }

    }

}
