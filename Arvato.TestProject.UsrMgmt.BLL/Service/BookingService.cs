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
       public List<Booking> GetUserOwnBooking(string userid)
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
       public void AddBooking(User user, Booking booking)
        {
            try
            {
                bookingRepository.AddBooking(user, booking);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public void ViewBooking(User user,Booking booking)
        {
            try
            {
             
                    bookingRepository.ViewBooking(user, booking);
                
                if (booking.ID == 0)
                {
                    throw (new Exception("your refrence number is not valid."));
                    
                }

                
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public void EditBooking(User user, Booking booking)
        {
            try
            {
                bookingRepository.EditBooking(user, booking);
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
