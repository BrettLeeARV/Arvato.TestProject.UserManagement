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
        IQueryable<Booking> GetUserOwnBooking(int userid);
        bool AddBooking(Booking booking);
        IQueryable<Booking> ViewBooking(Booking booking);
        bool EditBooking(Booking booking);
        void CancelBooking(Booking booking);
        IQueryable<string> CheckBookingAvailability(Booking booking, string AssetList, string Type);

        /// <summary>
        /// Checks if the selected room clashes with existing bookings.
        /// </summary>
        /// <param name="bookingID">ID of the booking. If this is a new booking, pass zero (0) as the parameter.</param>
        /// <param name="startDate">Start date of the booking.</param>
        /// <param name="endDate">End date of the booking.</param>
        /// <param name="roomID">ID of the room to book.</param>
        /// <returns></returns>
        IQueryable<Booking> CheckRoomAvailability(int bookingID, DateTime startDate, DateTime endDate, int roomID);

        /// <summary>
        /// Checks if the selected assets clash with existing bookings.
        /// </summary>
        /// <param name="bookingID">ID of the booking. If this is a new booking, pass zero (0) as the parameter.</param>
        /// <param name="startDate">Start date of the booking.</param>
        /// <param name="endDate">End date of the booking.</param>
        /// <param name="assetIDs">Array of asset IDs to book.</param>
        /// <returns></returns>
        IQueryable<AssetBooking> CheckAssetAvailability(int bookingID, DateTime startDate, DateTime endDate, int[] assetIDs);

        IQueryable<Booking> GetListByFilters(DateTime start, DateTime end, int userId, int roomId, bool isCanceled);
    }
}
