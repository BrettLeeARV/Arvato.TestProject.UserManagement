using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class RoomRepository : BaseRepository, IRoomRepository
    {
        static string connString = String.Empty;

        #region Constructors

        public RoomRepository()
            : base()
        {

        }

        public RoomRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
        }

        #endregion

        #region IBaseRepository members

        public IEnumerable<Room> GetAll()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var specificFields = session.QueryOver<Room>().List<Room>();
                    return specificFields.AsQueryable<Room>();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Add(Room entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Save(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }

        public void Update(Room entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Update(entity);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Room entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Delete(entity);
                    session.Flush();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IRoomRepository members

        public IEnumerable<Room> GetAllEnabled()
        {
            using (var session = NHibernateHelper.OpenSession(connString))
            {
                var roomList = session.QueryOver<Room>().Where(x => x.IsEnabled == true).OrderBy(x => x.Name).Asc.List();

                return roomList.AsQueryable<Room>();
            }
        }

        public IEnumerable<Room> GetRoomByID(int RoomID)
        {
            using (var session = NHibernateHelper.OpenSession(connString))
            {
                var roomList = session.QueryOver<Room>().Where(x => x.ID == RoomID).List();

                return roomList.AsQueryable<Room>();
            }
        }

        #endregion

    }
}
