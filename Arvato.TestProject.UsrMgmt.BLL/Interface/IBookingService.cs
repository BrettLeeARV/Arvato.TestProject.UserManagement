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
        void AddBooking(Booking booking);
        void ViewBooking(Booking booking);
        void EditBooking(Booking booking);
        void CancelBooking(Booking booking);
        List<Booking> GetUserOwnBooking(int userid);
    }
}
