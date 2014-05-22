using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface IBookingService : IDisposable
    {
        List<Booking> GetList();
        void AddBooking(User user, Booking booking);
        void ViewBooking(User user, Booking booking);
        void EditBooking(User user, Booking booking);
        void CancelBooking(Booking booking);
    }
}
