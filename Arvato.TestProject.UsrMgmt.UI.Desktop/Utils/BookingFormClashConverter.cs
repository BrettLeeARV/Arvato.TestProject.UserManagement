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
    public class BookingFormClashConverter : BaseConverter, IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
                          System.Globalization.CultureInfo culture)
        {
            Booking booking = null;
            try
            {
                booking = (Booking)value;
            }
            catch (Exception e)
            {
                throw new ArgumentException("Invalid value type", e);
            }

            var sb = new StringBuilder();
            // Start date and time
            sb.Append( booking.StartDate.ToString("g") );
            sb.Append(" - ");
            // If start and end are on different dates, show date and time
            if (booking.StartDate.Date != booking.EndDate.Date)
            {
                sb.Append(booking.EndDate.ToString("g") );
            }
            // ... otherwise, just show time
            else
            {
                sb.Append(booking.EndDate.ToShortTimeString());
            }
            return sb.ToString();
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
