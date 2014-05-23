using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentNHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Linq;
namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class UserRepository: BaseRepository, IUserRepository 
    {
        SessionFactory sessionfactroy;
        public UserRepository()
            : base()
        {
             
        }

        public UserRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            sessionfactroy = new SessionFactory();
        }

        #region IUserRepository Implementations

        public IQueryable<User> GetAll()
        {
            DataTable resultQuery = null;
            resultQuery = executeSelectQuery("USP_USER_SELECTDETAILS", null);

            var userList = new List<User>(resultQuery.Rows.Count);

            //Manual Mapping of DT columns into Entity classes
            foreach (DataRow currentRow in resultQuery.Rows)
            {
                var values = currentRow.ItemArray;
                var user = new User()
                {
                    ID = Convert.ToInt32(currentRow["ID"]),
                    FirstName = currentRow["FirstName"].ToString(),
                    LastName = currentRow["LastName"].ToString(),
                    Email = currentRow["Email"].ToString(),
                    LoginID = currentRow["LoginID"].ToString()
                };
                userList.Add(user);
            }

            return userList.AsQueryable<User>();

        }
        
        #endregion


        public bool Add(User entity)
        {
          
           // bool addrow = false;

            //try
            //{

            //    SqlParameter[] parameters = {new SqlParameter("@FirstName",SqlDbType.NVarChar,50) {Value = entity.FirstName},
            //                                new SqlParameter("@LastName",SqlDbType.NVarChar,50) { Value = entity.LastName},
            //                                new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = entity.Email},
            //                                new SqlParameter("@LoginID", SqlDbType.NVarChar, 50) { Value = entity.LoginID},
            //                                new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = entity.Password}};
            //    addrow = executeInsertQuery("USP_USER_REGISTER", parameters);
            //}
            //catch (Exception)
            //{

            //    throw;
            //}

            //return Convert.ToBoolean(addrow);
            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();

                using (var session = factory.OpenSession())
                {
                    var user = new User
                    {
                        FirstName = entity.FirstName,
                        LastName = entity.LastName,
                        Email = entity.Email,
                        LoginID = entity.LoginID,
                        Password = entity.Password
                    };
                    session.Save(user);
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
                throw new Exception(ex.Message);
            }
  

        }
        public void Login(User entity) // Added by Ben.
        {
           // int checkuser = 0;
            try
            {
                //   SqlParameter[] parameters = {new SqlParameter("@LoginID",SqlDbType.NVarChar,50) {Value = entity.LoginID},
                //                               new SqlParameter("@Password" , SqlDbType.NVarChar,50) {Value = entity.Password}};
                //DataTable dt = executeSelectQuery("USP_USER_LOGIN", parameters);
                //foreach (DataRow dr in dt.Rows)
                //{
                //    entity.ID = int.Parse(dr["ID"].ToString());
                //    entity.FirstName = dr["FirstName"].ToString();
                //    entity.LastName = dr["LastName"].ToString();
                //    entity.Email = dr["Email"].ToString();
                //    entity.LoginID = dr["LoginID"].ToString();
                //    entity.Password = dr["Password"].ToString();

                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();
                using (var session = factory.OpenSession())
                {
                    var userlist = session.Query<User>().Where(x => x.LoginID == entity.LoginID && x.Password == entity.Password).ToList();
                    if (userlist.AsQueryable<User>().Count() > 0)
                    {
                        foreach (User u in userlist.ToList<User>())
                        {
                            entity.ID = u.ID;
                            entity.FirstName = u.FirstName;
                            entity.LastName = u.LastName;
                            entity.Email = u.Email;
                            entity.LoginID = u.LoginID;
                            entity.Password = u.Password;
                        }
                    }
              
                }

                
                
            }
            catch (Exception)
            {
                throw;
            }
             
        }
        public void userDetails(User entity)
        {
            try
            {
               
            }
            catch (Exception)
            {
                throw;
            }
        }
        public void Update(User entity)
        {
            //Added by Ben.
            bool result = false;
            try
            {
                SqlParameter[] parameters = {new SqlParameter("@ID",SqlDbType.Int) {Value = entity.ID},
                                               new SqlParameter("@FirstName", SqlDbType.NVarChar,50) {Value = entity.FirstName},
                                               new SqlParameter("@LastName", SqlDbType.NVarChar,50) {Value = entity.LastName},
                                               new SqlParameter("@Email", SqlDbType.NVarChar,50) {Value = entity.Email},
                                               new SqlParameter("@LoginID", SqlDbType.NVarChar,50) {Value= entity.LoginID},
                                               new SqlParameter("@Password", SqlDbType.NVarChar,50) {Value = entity.Password}};
                result = executeUpdateQuery("USP_USER_UPDATE", parameters);
            }
            catch (Exception)
            {
                
                throw;
            }

            //throw new NotImplementedException();
        }

        public bool Delete(User entity)
        {
            bool deleterow = false;

            try
            {
                SqlParameter[] parameters = {new SqlParameter("@ID",SqlDbType.Int) {Value = entity.ID}};
                deleterow = executeDeleteQuery("USP_USER_DELETE", parameters);
            }
            catch (Exception)
            {
                throw;
            }

            return Convert.ToBoolean(deleterow);
        }

        public bool IsExistingLoginID(string LoginID,int ID) // Added by Beh.
        {
            // int checkuser = 0;
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
       
    }
}
