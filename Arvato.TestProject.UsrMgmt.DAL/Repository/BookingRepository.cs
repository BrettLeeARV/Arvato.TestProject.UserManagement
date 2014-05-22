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

                SqlParameter[] paramiters = {new SqlParameter("@UserID", SqlDbType.Int) {Value = entity.ID},
                                                new SqlParameter("@RoomID", SqlDbType.Int) {Value = booking.roomID},
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

        public bool ViewBooking(User entity,Booking booking)
        {
            //bool result = false;
            try
            {
                DataTable dt = null;
                SqlParameter[] paramiters = {new SqlParameter("@RefNum", SqlDbType.Int) {Value = booking.refNum}};

                dt = executeSelectQuery("USP_VIEW_BOOKING", paramiters);
                foreach (DataRow dr in dt.Rows)
                {
                    booking.ID = int.Parse(dr["ID"].ToString());
                    entity.LoginID = dr["LoginID"].ToString();
                    booking.roomID = (int.Parse)(dr["RoomID"].ToString());
                    booking.startDate = DateTime.Parse(dr["StartDate"].ToString());
                    booking.endDate = DateTime.Parse(dr["EndDate"].ToString());
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }
        public bool EditBooking(User entity, Booking booking)
        {
            bool result = false;

            try
            {
                SqlParameter[] paramiters = {new SqlParameter("@ID",SqlDbType.Int){Value = booking.ID},
                                                new SqlParameter("@RoomID", SqlDbType.TinyInt){Value = booking.roomID},
                                                new SqlParameter("@StartDate" , SqlDbType.DateTime) { Value = booking.startDate},
                                                new SqlParameter("@EndDate" , SqlDbType.DateTime) { Value = booking.endDate}};

                result = executeUpdateQuery("USP_EDIT_BOOKING", paramiters);
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
        }
        public bool CancelBooking(Booking booking)
        {
            bool result = false;
            try
            {
                SqlParameter[] paramiters = { new SqlParameter("@ID", SqlDbType.Int) { Value = booking.ID } };
                result = executeUpdateQuery("USP_CANCEL_BOOKING", paramiters);
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
     
        }
    }
}
