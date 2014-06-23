using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using NHibernate.Linq;

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
                    var Fields = session.CreateQuery("FROM User").List<User>();
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
            bool result = false;
            try
            {
                SqlParameter[] parameters = {new SqlParameter("@ID",SqlDbType.Int) {Value = entity.ID},
                                               new SqlParameter("@FirstName", SqlDbType.NVarChar,50) {Value = entity.FirstName},
                                               new SqlParameter("@LastName", SqlDbType.NVarChar,50) {Value = entity.LastName},
                                               new SqlParameter("@Email", SqlDbType.NVarChar,50) {Value = entity.Email},
                                               new SqlParameter("@LoginID", SqlDbType.NVarChar,50) {Value= entity.LoginID},
                                               new SqlParameter("@Password", SqlDbType.NVarChar,70) {Value = hash.CreateHash(entity.Password)}};
                result = executeUpdateQuery("USP_USER_UPDATE", parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool Delete(User entity)
        {
            bool deleterow = false;

            try
            {
                SqlParameter[] parameters = { new SqlParameter("@ID", SqlDbType.Int) { Value = entity.ID } };
                deleterow = executeDeleteQuery("USP_USER_DELETE", parameters);
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

                    if (user.IsWindowAuthenticate == false && hash.ValidatePassword(entity.Password,user.Password))
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
                SqlParameter[] parameters = { new SqlParameter("@LoginID", SqlDbType.NVarChar, 50) { Value = LoginID},
                                                new SqlParameter("@ID", SqlDbType.Int) { Value = ID}};
                DataTable dt = executeSelectQuery("USP_USER_VALIDATE_LOGINID", parameters);
                if (dt.Rows.Count > 0)
                    return true;
                else
                    return false;
            }
            catch (Exception)
            {
                throw;
            }

        }

        #endregion

    }
}
