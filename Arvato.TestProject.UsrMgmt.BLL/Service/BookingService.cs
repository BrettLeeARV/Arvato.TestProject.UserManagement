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
                    else
                    {
                        List<AssetBooking> assetList = new List<AssetBooking>();
                        foreach (AssetBooking asset in book.AssetBookings)
                        {
                            if (asset.Status == 1)
                            {
                                assetList.Add(asset);
                            }
                        }

                        book.AssetBookings = assetList;
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
            try
            {
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
