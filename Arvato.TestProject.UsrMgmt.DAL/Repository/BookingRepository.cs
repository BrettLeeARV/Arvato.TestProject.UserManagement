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
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        public BookingRepository(SqlConnection dbConnection):base(dbConnection)
        {
            
        }

        public IQueryable<Booking> GetList()
        {
            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();
                using (var session = factory.OpenSession())
                {
                    var specificFields = session.CreateQuery("FROM Booking").List<Booking>();

                    return specificFields.AsQueryable<Booking>();
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }
        public IQueryable<Booking> GetUserOwnBooking(int userid) // TO Do For Tomorrow.
        {
            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();
                using (var session = factory.OpenSession())
                {
                    var specificFields = session.CreateQuery("FROM Booking WHERE UserID = '" + userid + "'").List<Booking>();

                    return specificFields.AsQueryable<Booking>();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }
        public bool AddBooking(Booking booking) // Added by Ben
        {
            bool result = false;

            try
            {
                SqlParameter outputParameter = new SqlParameter();
                outputParameter.ParameterName = "@RefNum";
                outputParameter.SqlDbType = System.Data.SqlDbType.VarChar;
                outputParameter.Size = 50;
                outputParameter.Direction = ParameterDirection.Output;

                SqlParameter[] paramiters = {new SqlParameter("@UserID", SqlDbType.Int) {Value = booking.UserID},
                                                new SqlParameter("@RoomID", SqlDbType.Int) {Value = booking.RoomID},
                                                new SqlParameter("@StartDate", SqlDbType.DateTime) {Value = booking.StartDate},
                                                new SqlParameter("@EndDate" , SqlDbType.DateTime) {Value = booking.EndDate}, outputParameter};

                object returnValue = null;
                result = executeInsertQuery("USP_MAKE_BOOKING", paramiters, ref returnValue);

                booking.RefNum = returnValue.ToString();


            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public bool ViewBooking(Booking booking)
        {
            //bool result = false;
            try
            {
                DataTable dt = null;
                SqlParameter[] paramiters = {new SqlParameter("@RefNum", SqlDbType.VarChar,50) {Value = booking.RefNum}};

                dt = executeSelectQuery("USP_VIEW_BOOKING", paramiters);
                foreach (DataRow dr in dt.Rows)
                {
                    booking.ID = int.Parse(dr["ID"].ToString());
                    booking.RoomID = (int.Parse)(dr["RoomID"].ToString());
                    booking.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                    booking.EndDate = DateTime.Parse(dr["EndDate"].ToString());
                }

                return true;

            }
            catch (Exception ex)
            {
                return false;
                throw ex;
            }

        }
        public bool EditBooking(Booking booking)
        {
            bool result = false;

            try
            {
                SqlParameter[] paramiters = {new SqlParameter("@ID",SqlDbType.Int){Value = booking.ID},
                                                new SqlParameter("@RoomID", SqlDbType.TinyInt){Value = booking.RoomID},
                                                new SqlParameter("@StartDate" , SqlDbType.DateTime) { Value = booking.StartDate},
                                                new SqlParameter("@EndDate" , SqlDbType.DateTime) { Value = booking.EndDate}};

           //     result = executeUpdateQuery("USP_EDIT_BOOKING", paramiters);
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
              //  result = executeUpdateQuery("USP_CANCEL_BOOKING", paramiters);
            }
            catch (Exception)
            {
                
                throw;
            }
            return result;
     
        }
    }
}
