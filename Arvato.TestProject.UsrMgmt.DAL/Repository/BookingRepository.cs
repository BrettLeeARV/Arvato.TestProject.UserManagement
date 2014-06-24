using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using NHibernate;
using NHibernate.Linq;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class BookingRepository : BaseRepository, IBookingRepository
    {
        static string connString = String.Empty;

        #region Constructors

        public BookingRepository()
            : base()
        {

        }

        public BookingRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
        }

        #endregion

        #region IBaseRepository members

        public IEnumerable<Booking> GetAll()
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

        public bool Add(Booking booking)
        {
            bool result = false;

            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var book = session.CreateSQLQuery("EXEC USP_MAKE_BOOKING :UserID, :RoomID, :StartDate, :EndDate")
                           .AddEntity(typeof(Booking))
                           .SetParameter("UserID", booking.UserID)
                           .SetParameter("RoomID", booking.RoomID)
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
                           .SetParameter("AssetID", asset.AssetID)
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

        public void Update(Booking booking)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var book = session.CreateSQLQuery("EXEC USP_EDIT_BOOKING :ID, :RoomID, :StartDate, :EndDate, :ModifiedBy")
                           .SetParameter("ID", booking.ID)
                           .SetParameter("RoomID", booking.RoomID, NHibernateUtil.Int32)
                           .SetParameter("StartDate", booking.StartDate)
                           .SetParameter("EndDate", booking.EndDate)
                           .SetParameter("ModifiedBy", booking.ModifiedBy)
                           .UniqueResult();

                    foreach (AssetBooking asset in booking.AssetBookings)
                    {
                        var assetBooking = session.CreateSQLQuery("EXEC USP_SAVE_ASSET_BOOKING :BookingID, :AssetID, :Status")
                       .SetParameter("BookingID", booking.ID)
                       .SetParameter("AssetID", asset.AssetID)
                       .SetParameter("Status", asset.Status)
                       .UniqueResult();
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(Booking entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IBookingRepository members

        public void CancelBooking(Booking booking)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var update = session.CreateSQLQuery("USP_CANCEL_BOOKING :ID")
                        .SetParameter("ID", booking.ID)
                        .UniqueResult();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public IEnumerable<Booking> GetUserOwnBooking(int userid)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var specificFields = session.QueryOver<Booking>().Where(x => x.UserID == userid).List();
                    return specificFields.AsQueryable<Booking>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        // TODO: find out if this method is really needed, doesn't seem to make sense
        public IEnumerable<Booking> ViewBooking(Booking booking)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var bookingList = session.Query<Booking>().Where(x => x.RefNum == booking.RefNum).ToList();

                    return bookingList.AsQueryable<Booking>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Booking> CheckRoomAvailability(int bookingID, DateTime startDate, DateTime endDate, int roomID)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var bookingList = session.CreateSQLQuery("EXEC USP_ROOM_AVAILABILITY :ID, :StartDate, :EndDate, :RoomID")
                           .AddEntity(typeof(Booking))
                           .SetParameter("ID", bookingID)
                           .SetParameter("StartDate", startDate)
                           .SetParameter("EndDate", endDate)
                           .SetParameter("RoomID", roomID, NHibernateUtil.Int32);
                    var list = bookingList.List<Booking>();

                    return list.AsQueryable<Booking>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Booking> CheckAssetAvailability(int bookingID, DateTime startDate, DateTime endDate, int[] assetIDs)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var bookingList = session.CreateSQLQuery("EXEC USP_ASSET_AVAILABILITY :ID, :StartDate, :EndDate, :AssetIDs")
                           .AddEntity(typeof(Booking))
                           .SetParameter("ID", bookingID)
                           .SetParameter("StartDate", startDate)
                           .SetParameter("EndDate", endDate)
                           .SetParameter("AssetIDs", String.Join("|", assetIDs));
                    var list = bookingList.List<Booking>();

                    return list.AsQueryable<Booking>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Booking> GetListByFilters(DateTime start, DateTime end, int userId, int roomId, bool isCanceled)
        {
            try
            {
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
                        bookingList.Where(x => x.UserID == userId);
                    }
                    // if set, filter by room
                    if (roomId > 0)
                    {
                        bookingList.Where(x => x.RoomID == roomId);
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

        public IEnumerable<string> getBookedItems(int bookingID, DateTime startDate, DateTime endDate)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var bookingList = session.CreateSQLQuery("EXEC USP_ROOM_ASSET_AVAILABILITY :ID, :StartDate, :EndDate")
                           .SetParameter("ID",bookingID)
                           .SetParameter("StartDate", startDate)
                           .SetParameter("EndDate", endDate);
                    var list = bookingList.List<string>();

                    return list.AsQueryable<string>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

    }

}