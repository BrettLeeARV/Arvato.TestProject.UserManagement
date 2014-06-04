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
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        static string connString = "";

        public BookingRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
        }

        public IQueryable<Booking> GetList()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var specificFields = session.QueryOver<Booking>().List<Booking>();

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
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    // var specificFields = session.CreateQuery("FROM Booking WHERE UserID = '" + userid + "'").List<Booking>();
                    //var specificFields = session.QueryOver<Booking>().Where(x => x.UserID == userid).List();
                    var specificFields = session.QueryOver<Booking>().Where(x => x.User.ID == userid).List();

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
                //SqlParameter outputParameter = new SqlParameter();
                //outputParameter.ParameterName = "@RefNum";
                //outputParameter.SqlDbType = System.Data.SqlDbType.VarChar;
                //outputParameter.Size = 50;
                //outputParameter.Direction = ParameterDirection.Output;

                //SqlParameter[] paramiters = {new SqlParameter("@UserID", SqlDbType.Int) {Value = booking.UserID},
                //                                new SqlParameter("@RoomID", SqlDbType.Int) {Value = booking.RoomID},
                //                                new SqlParameter("@StartDate", SqlDbType.DateTime) {Value = booking.StartDate},
                //                                new SqlParameter("@EndDate" , SqlDbType.DateTime) {Value = booking.EndDate}, outputParameter};

                //object returnValue = null;
                //result = executeInsertQuery("USP_MAKE_BOOKING", paramiters, ref returnValue);

                //booking.RefNum = returnValue.ToString();

                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var book = session.CreateSQLQuery("EXEC USP_MAKE_BOOKING :UserID, :RoomID, :StartDate, :EndDate")
                           .AddEntity(typeof(Booking))
                           .SetParameter("UserID", booking.User.ID)
                           .SetParameter("RoomID", booking.Room.ID)
                           .SetParameter("StartDate", booking.StartDate)
                           .SetParameter("EndDate", booking.EndDate)
                           .List<Booking>();

                    if (book.AsQueryable<Booking>().Count() > 0)
                    {
                        Booking detail = book.SingleOrDefault<Booking>();

                        foreach (AssetBooking asset in booking.AssetBookings)
                        {
                            var assetBooking = session.CreateSQLQuery("EXEC USP_SAVE_ASSET_BOOKING :BookingID, :AssetID, :Status")
                           .SetParameter("BookingID", detail.ID)
                           .SetParameter("AssetID", asset.Asset.ID)
                           .SetParameter("Status", asset.Status)
                           .UniqueResult();
                        }

                        booking.RefNum = detail.RefNum;
                    }
                }

            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public IQueryable<Booking> ViewBooking(Booking booking)
        {
            //bool result = false;
            try
            {
                //  DataTable dt = null;
                //  SqlParameter[] paramiters = {new SqlParameter("@RefNum", SqlDbType.VarChar,50) {Value = booking.RefNum}};



                ////  dt = executeSelectQuery("USP_VIEW_BOOKING", paramiters);
                //  foreach (DataRow dr in dt.Rows)
                //  {
                //      booking.ID = int.Parse(dr["ID"].ToString());
                //      booking.RoomID = (int.Parse)(dr["RoomID"].ToString());
                //      booking.StartDate = DateTime.Parse(dr["StartDate"].ToString());
                //      booking.EndDate = DateTime.Parse(dr["EndDate"].ToString());
                //  }

                //  return true;

                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var bookingList = session.Query<Booking>().Where(x => x.IsCanceled == false && x.RefNum == booking.RefNum).ToList();

                    return bookingList.AsQueryable<Booking>();
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
        public bool EditBooking(Booking booking)
        {
            bool result = false;

            try
            {
                //SqlParameter[] paramiters = {new SqlParameter("@ID",SqlDbType.Int){Value = booking.ID},
                //                                new SqlParameter("@RoomID", SqlDbType.TinyInt){Value = booking.RoomID},
                //                                new SqlParameter("@StartDate" , SqlDbType.DateTime) { Value = booking.StartDate},
                //                                new SqlParameter("@EndDate" , SqlDbType.DateTime) { Value = booking.EndDate}};

                //     result = executeUpdateQuery("USP_EDIT_BOOKING", paramiters);

                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var book = session.CreateSQLQuery("EXEC USP_EDIT_BOOKING :ID, :RoomID, :StartDate, :EndDate")
                           .SetParameter("ID", booking.ID)
                           .SetParameter("RoomID", booking.Room.ID, NHibernateUtil.Int32)
                           .SetParameter("StartDate", booking.StartDate)
                           .SetParameter("EndDate", booking.EndDate)
                           .UniqueResult();

                    foreach (AssetBooking asset in booking.AssetBookings)
                    {
                        var assetBooking = session.CreateSQLQuery("EXEC USP_SAVE_ASSET_BOOKING :BookingID, :AssetID, :Status")
                       .SetParameter("BookingID", booking.ID)
                       .SetParameter("AssetID", asset.Asset.ID)
                       .SetParameter("Status", asset.Status)
                       .UniqueResult();
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }
        //public bool CancelBooking(Booking booking)
        //{
        //    bool result = false;
        //    try
        //    {
        //        SqlParameter[] paramiters = { new SqlParameter("@ID", SqlDbType.Int) { Value = booking.ID } };
        //        result = executeUpdateQuery("USP_CANCEL_BOOKING", paramiters);
        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return result;
        public void CancelBooking(Booking booking)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {

                    var update = session.CreateSQLQuery("USP_CANCEL_BOOKING :ID")
                        .SetParameter("ID", booking.ID)
                        .UniqueResult();
                    // session.SaveOrUpdate(booking);
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public IQueryable<string> CheckBookingAvailability(Booking booking, string AssetList, string Type)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {

                    var bookingList = session.CreateSQLQuery("EXEC USP_ROOM_ASSET_AVAILABILITY :ID, :StartDate, :EndDate, :RoomID, :AssetID, :Type")
                           .SetParameter("ID", booking.ID)
                           .SetParameter("StartDate", booking.StartDate)
                           .SetParameter("EndDate", booking.EndDate)
                           .SetParameter("RoomID", booking.Room.ID, NHibernateUtil.Int32)
                           .SetParameter("AssetID", AssetList)
                           .SetParameter("Type", Type)
                           .List<string>();

                    return bookingList.AsQueryable<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IQueryable<Booking> GetListByFilters(DateTime start, DateTime end, int userId, int roomId, bool isCanceled)
        {
            try
            {
                // FIX ME
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var bookingList = session.QueryOver<Booking>();
                    // filter by date
                    bookingList.Where(x =>
                        (x.StartDate >= start && x.StartDate < end)
                        || (x.EndDate >= start && x.EndDate < end)
                    );
                    // if set, filter by user
                    if (userId > 0)
                    {
                        //bookingList.Where(x => x.UserID == userId);
                        bookingList.Where(x => x.User.ID == userId);
                    }
                    // if set, filter by room
                    if (roomId > 0)
                    {
                        //bookingList.Where(x => x.RoomID == roomId);
                        bookingList.Where(x => x.Room.ID == roomId);
                    }
                    // filter by isCanceled
                    if (isCanceled)
                    {
                        // do nothing and both canceled and non-canceled bookings will be returned
                    }
                    else
                    {
                        bookingList.Where(x => x.IsCanceled == false); // only non-canceled bookings
                    }

                    // force a default ordering for now
                    return bookingList.OrderBy(x => x.StartDate).Asc.List<Booking>().AsQueryable();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }

}
