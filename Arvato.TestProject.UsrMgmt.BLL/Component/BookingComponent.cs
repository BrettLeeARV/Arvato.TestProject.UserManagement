﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.DAL.Repository;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;

namespace Arvato.TestProject.UsrMgmt.BLL.Component
{
    public class BookingComponent : IBookingComponent
    {
        #region Fields
        IBookingRepository bookingRepository;
        #endregion

        #region Constructor
        public BookingComponent()
        {
            bookingRepository = new BookingRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
        }
        #endregion

        #region IBookingService Implementation

        public List<Booking> GetList()
        {
            try
            {
#if DEBUG
                Thread.Sleep(3000);
#endif
                return bookingRepository.GetAll().ToList<Booking>();
            }
            catch (Exception ex)
            {
                //Insert error Logging/Handling Mechanism here
                throw ex;
            }

        }
        public List<Booking> GetUserOwnBooking(int userid)
        {
            try
            {
                return bookingRepository.GetUserOwnBooking(userid).ToList<Booking>();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public void AddBooking(Booking booking)
        {
            try
            {
                bookingRepository.Add(booking);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ViewBooking(ref Booking booking)
        {
            try
            {

                List<Booking> detail = bookingRepository.ViewBooking(booking).ToList();

                foreach (Booking book in detail)
                {
                    if (book.ID == 0)
                    {
                        throw (new Exception("your refrence number is not valid."));

                    }

                    booking = book;
                }

            }
            catch (Exception)
            {
                throw;
            }
        }
        public void EditBooking(Booking booking)
        {
            try
            {
                bookingRepository.Update(booking);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void CancelBooking(Booking booking)
        {
            try
            {
                if (booking.ID > 0)
                {
                    bookingRepository.CancelBooking(booking);
                }
                else
                {

                }
                //return true;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public void Save(Booking booking)
        {
#if DEBUG
            Thread.Sleep(3000);
#endif
            try
            {
                if (booking.StartDate.ToShortDateString() == "1/1/0001")
                    throw new Exception("StartDate is a require field");
                if (booking.EndDate.ToShortDateString() == "1/1/0001")
                    throw new Exception("EndDate is a require field");
                if(booking.StartDate >= booking.EndDate)
                    throw new Exception("EndDate must be greater than StartDate");
                if (booking.RoomID == 0 && booking.AssetBookings.Count == 0)
                    throw new Exception("Please select a room or asset to book");

                // Check if room is available
                var conflictingBookings = CheckRoomAvailability(booking);
                if (conflictingBookings.Count() > 0)
                {
                    throw new RoomClashException() { Clashes = conflictingBookings };
                }

                // Check if assets are available
                var conflictingAssetBookings = CheckAssetAvailability(booking);
                if (conflictingAssetBookings.Count() > 0)
                {
                    throw new AssetClashException() { Clashes = conflictingAssetBookings };
                }

                if (booking.ID > 0)
                {
                    bookingRepository.Update(booking);
                }
                else
                {
                    bookingRepository.Add(booking);
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Booking> GetListByFilters(DateTime start, DateTime end, int userId, int roomId, bool isCanceled)
        {
            try
            {
#if DEBUG
                //Thread.Sleep(3000);
#endif
                // add 1 day, so that a search for 1/1/2001 to 1/1/2001, will actually be performed as 1/1/2001 to 2/1/2001,
                // which will produce the expected result
                end = end.AddDays(1);
                return bookingRepository.GetListByFilters(start, end, userId, roomId, isCanceled).ToList();
            }
            catch (Exception ex)
            {
                //Insert error Logging/Handling Mechanism here
                throw ex;
            }
        }

        public List<Booking> CheckRoomAvailability(Booking booking)
        {
            List<Booking> conflicts = bookingRepository.CheckRoomAvailability(booking.ID, booking.StartDate, booking.EndDate, booking.RoomID).ToList();
            return conflicts;
        }

        public List<Booking> CheckAssetAvailability(Booking booking)
        {
            int[] assetIDs = booking.AssetBookings.Select(x => x.AssetID).ToArray();
            List<Booking> conflicts = bookingRepository.CheckAssetAvailability(booking.ID, booking.StartDate, booking.EndDate, assetIDs).ToList();
            return conflicts;
        }

        public List<string> getBookedItem(Booking booking)
        {
            List<string> bookedItems = bookingRepository.getBookedItems(booking.ID, booking.StartDate, booking.EndDate).ToList();
            return bookedItems;
        }

        #endregion

        #region IDisposable Implementation
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    bookingRepository.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        #endregion

    }
}
