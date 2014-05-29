﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IBookingRepository : IDisposable
    {
        IQueryable<Booking> GetList();
        IQueryable<Booking> GetUserOwnBooking(int userid);
        bool AddBooking(Booking booking);
        IQueryable<Booking> ViewBooking(Booking booking);
        bool EditBooking(Booking booking);
        void CancelBooking(Booking booking);
    }
}
