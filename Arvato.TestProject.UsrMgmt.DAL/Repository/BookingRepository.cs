using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity;
using System.Data;
using Arvato.TestProject.UsrMgmt.Entity.Model;

using System.Data.SqlClient;
using System.Configuration;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
   public class BookingRepository : BaseRepository,IBookingRepository
    {
        public BookingRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
        }

        public bool AddBooking(User entity, Booking booking) // Added by Ben
        {
            bool result = false;

            try
            {
                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@RefNum";
                outputParameter.SqlDbType = System.Data.SqlDbType.Int;
                outputParameter.Direction = ParameterDirection.Output;

                SqlParameter[] paramiters = {new SqlParameter("@LoginID", SqlDbType.NVarChar,50) {Value = entity.LoginID},
                                                new SqlParameter("@RoomID", SqlDbType.TinyInt) {Value = booking.roomID},
                                                new SqlParameter("@StartDate", SqlDbType.DateTime) {Value = booking.startDate},
                                                new SqlParameter("@EndDate" , SqlDbType.DateTime) {Value = booking.endDate}, outputParameter};

                object returnValue = null;
                result = executeInsertQuery("USP_MAKE_BOOKING", paramiters, ref returnValue);

                booking.refNum = returnValue.ToString();


            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
    }
}
