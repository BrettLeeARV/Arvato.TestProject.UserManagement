using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Services.ConsoleApp.ServiceReference1;

namespace Arvato.TestProject.UsrMgmt.Services.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BookingServiceClient client = new BookingServiceClient();
            BookingsData data = new BookingsData();
            data.Start = new DateTime(2014, 1, 1);
            data.End = new DateTime(2014, 12, 31);

            data = client.GetBookings(data);

            Console.WriteLine("Number of bookings: " + data.Bookings.Count());
            Console.ReadLine();
        }
    }
}
