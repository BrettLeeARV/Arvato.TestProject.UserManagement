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
    public class UserRepository: BaseRepository, IUserRepository 
    {
        public UserRepository()
            : base()
        {
        }

        public UserRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
        }

        #region IUserRepository Implementations

        public IQueryable<User> GetAll()
        {
            DataTable resultQuery = null;
            resultQuery = executeSelectQuery("SELECT * " +
                                             "FROM dbo.[USER]", null);

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
          
            bool addrow = false;

            try
            {
                
                  SqlParameter[] parameters = {new SqlParameter("@FirstName",SqlDbType.NVarChar,50) {Value = entity.FirstName},
                                            new SqlParameter("@LastName",SqlDbType.NVarChar,50) { Value = entity.LastName},
                                            new SqlParameter("@Email", SqlDbType.NVarChar, 50) { Value = entity.Email},
                                            new SqlParameter("@LoginID", SqlDbType.NVarChar, 50) { Value = entity.LoginID},
                                            new SqlParameter("@Password", SqlDbType.NVarChar, 50) { Value = entity.Password}};
                                  addrow = executeInsertQuery("USP_USER_REGISTER", parameters);
            }
            catch (Exception)
            {

                throw;
            }
            finally
            {
                Connection.Close();

            }
            return Convert.ToBoolean(addrow);
        }
        public int Login(User entity) // Added by Ben.
        {
            int checkuser = 0;
            try
            {
                SqlParameter[] parameters = {new SqlParameter("@LoginID",SqlDbType.NVarChar,50) {Value = entity.LoginID},
                                               new SqlParameter("@Password" , SqlDbType.NVarChar,50) {Value = entity.Password}};
                checkuser = int.Parse(executeScalarquery("USP_USER_LOGIN", parameters).ToString());
            }
            catch (Exception)
            {
                
                throw;
            }
            return checkuser;
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
            throw new NotImplementedException();
        }

        public void Delete(User entity)
        {
            throw new NotImplementedException();
        }
    }
}
