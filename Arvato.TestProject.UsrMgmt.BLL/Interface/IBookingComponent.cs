﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface IBookingComponent : IDisposable
    {
        List<Booking> GetList();
        void AddBooking(Booking booking);
        void ViewBooking(ref Booking booking);
        void EditBooking(Booking booking);
        void CancelBooking(Booking booking);
        List<Booking> GetUserOwnBooking(int userid);
        void Save(Booking booking);
        List<Booking> GetListByFilters(DateTime start, DateTime end, int userId, int roomId, bool isCanceled);
        List<Booking> CheckRoomAvailability(Booking booking);
        List<Booking> CheckAssetAvailability(Booking booking);
        List<string> getBookedItem(Booking booking);
    }
}
