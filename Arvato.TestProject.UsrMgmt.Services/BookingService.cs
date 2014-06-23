using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Services.Contracts;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.Services.Contracts.DataContract;

namespace Arvato.TestProject.UsrMgmt.Services
{
    public class BookingService : Contracts.IBookingService
    {
        public IEnumerable<Booking> GetBookings(DateTime start, DateTime end, int userId, int roomId, bool isCanceled)
        {
            IBookingComponent component = new BookingComponent();

            var bookings = component.GetListByFilters(start, end, userId, roomId, isCanceled);

            return bookings;
        }

        public bool CancelBooking(Booking booking)
        {
            IBookingComponent component = new BookingComponent();

            try
            {
                component.CancelBooking(booking);
            }
            catch(Exception e)
            {
                throw new FaultException(e.Message);
            }
            return true;
        }

        public Booking SaveBooking(Booking booking)
        {
            IBookingComponent component = new BookingComponent();

            try
            {
                component.Save(booking);
            }
            catch (RoomClashException e)
            {
                var fault = new RoomClashFault() { Clashes = e.Clashes };
                throw new FaultException<RoomClashFault>(fault);
            }
            catch (AssetClashException e)
            {
                var fault = new AssetClashFault() { Clashes = e.Clashes };
                throw new FaultException<AssetClashFault>(fault);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            return booking;
        }

        public IEnumerable<Booking> CheckRoomAvailability(Booking booking)
        {
            IBookingComponent component = new BookingComponent();
            var clashes = component.CheckRoomAvailability(booking);
            return clashes;
        }

        public IEnumerable<Booking> CheckAssetAvailability(Booking booking)
        {
            IBookingComponent component = new BookingComponent();
            var clashes = component.CheckAssetAvailability(booking);
            return clashes;
        }

        public IEnumerable<string> getBookedItem(Booking booking)
        {
            IBookingComponent component = new BookingComponent();
            var clashes = component.getBookedItem(booking);
            return clashes;
        }

    }
}
