using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IBookingRepository : IDisposable
    {
        IQueryable<Booking> GetList();
        IQueryable<Booking> GetUserOwnBooking(string userid);
        bool AddBooking(User user, Booking booking);
        bool ViewBooking( User user,Booking booking);
        bool EditBooking(User user, Booking booking);
        bool CancelBooking(Booking booking);
    }
}
