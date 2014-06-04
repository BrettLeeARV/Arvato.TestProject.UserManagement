using System;
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

namespace Arvato.TestProject.UsrMgmt.BLL.Service
{
    public class BookingService : IBookingService
    {
        #region Fields
        IBookingRepository bookingRepository;
        #endregion

        #region Constructor
        public BookingService()
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
                return bookingRepository.GetList().ToList<Booking>();
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
                bookingRepository.AddBooking(booking);
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
                bookingRepository.EditBooking(booking);
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
                string assetList = "";
                foreach (AssetBooking List in booking.AssetBookings)
                {
                    assetList = assetList + "|" + List.Asset.ID;
                }

                if (booking.StartDate.ToShortDateString() == "1/1/0001")
                    throw new Exception("StartDate is a require field");
                if (booking.EndDate.ToShortDateString() == "1/1/0001")
                    throw new Exception("EndDate is a require field");
                if(booking.StartDate >= booking.EndDate)
                    throw new Exception("EndDate must be greater than StartDate");
                if ((booking.Room.ID == 0 || booking.Room.ID == null) && booking.AssetBookings.Count == 0)
                    throw new Exception("Please select a room or asset to book");

                string conflict = "";
                List<string> conflictBooking = bookingRepository.CheckBookingAvailability(booking, "", "Room").ToList();

                if (conflictBooking.Count() > 0)
                { 
                    foreach (string message in conflictBooking)
                    {
                        conflict = conflict + message + ";";
                    }
                    throw new Exception("Room booking conflict : " + conflict);
                }

                conflict = "";
                conflictBooking = bookingRepository.CheckBookingAvailability(booking, assetList, "Asset").ToList();

                if (conflictBooking.Count() > 0)
                {
                    foreach (string message in conflictBooking)
                    {
                        conflict = conflict + message + ";";
                    }
                    throw new Exception("Asset booking conflict : " + conflict);
                }

                if (booking.ID > 0)
                {
                    bookingRepository.EditBooking(booking);
                }
                else
                {
                    bookingRepository.AddBooking(booking);
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

        public List<string> CheckRoomAvailability(Booking booking, string assetList, string type)
        {
            List<string> conflictBooking = bookingRepository.CheckBookingAvailability(booking, "", "Room").ToList();
            return conflictBooking;
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
