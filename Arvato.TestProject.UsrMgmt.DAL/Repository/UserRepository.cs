using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using NHibernate.Linq;
using NHibernate.SqlCommand;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class UserRepository : BaseRepository, IUserRepository
    {
        static string connString = String.Empty;
        static PasswordHash hash = null;

        #region Constructors

        public UserRepository()
            : base()
        {

        }

        public UserRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
            hash = new PasswordHash();
        }

        #endregion

        #region IBaseRepository members

        public IEnumerable<User> GetAll()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    //var Fields = session.CreateQuery("FROM User").List<User>();
                    var Fields = session.QueryOver<User>()
                        .Fetch(a => a.Role).Eager
                        .List<User>();
                    return Fields.AsQueryable<User>();
                }
            }
            catch (Exception)
            {

                throw;
            }

        }

        public bool Add(User entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    entity.Password = hash.CreateHash(entity.Password);
                    session.Save(entity);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(User entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    entity.Password = hash.CreateHash(entity.Password);
                    session.Update(entity);
                    session.Flush();
                }
            }
            catch
            {
                throw;
            }
        }

        public bool Delete(User entity)
        {
            bool deleterow = false;

            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Delete(entity);
                    session.Flush();
                }
            }
            catch (Exception)
            {
                throw;
            }

            return Convert.ToBoolean(deleterow);
        }

        #endregion

        #region IUserRepository members

        public bool Login(User entity)
        {
            bool result = false;

            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    User user = session.Query<User>().Where(x => x.LoginID == entity.LoginID).Single();

                    if (user.IsWindowAuthenticate == false && hash.ValidatePassword(entity.Password, user.Password))
                    {
                        result = true;
                        entity.ID = user.ID;
                    }
                    else
                    {
                        result = false;
                        entity.IsWindowAuthenticate = user.IsWindowAuthenticate;
                        entity.ID = user.ID;
                    }
                }
            }
            catch (Exception)
            {
                throw;
            }
            return result;
        }

        public bool IsExistingLoginID(string LoginID, int ID)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var userList = session.CreateSQLQuery("EXEC USP_USER_VALIDATE_LOGINID :LoginID, :ID")
                           .AddEntity(typeof(Booking))
                           .SetParameter("LoginID", LoginID)
                           .SetParameter("ID", ID);
                    var list = userList.List<User>();

                    if (list.Count > 0)
                        return true;
                    else
                        return false;
                }
            }
            catch (Exception ex)
            {
                ex.Log();
                throw;
            }

        }

        public User GetUserByLoginID(string LoginID)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    User user = session.QueryOver<User>().Where(x => x.LoginID == LoginID)
                        .SingleOrDefault();

                    return user;
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<User> GetAllUsersWithRole()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var Fields = session.QueryOver<User>()
                        .Fetch(a => a.Role).Eager
                        .List<User>();
                    return Fields.AsQueryable<User>();
                }
            }
            catch
            {

                throw;
            }

        }
        #endregion

    }
}
