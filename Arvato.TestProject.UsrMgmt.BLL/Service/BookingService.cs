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
       IBookingRepository bookingRepository;
             public BookingService()
        {
            bookingRepository = new BookingRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
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
    }
}
