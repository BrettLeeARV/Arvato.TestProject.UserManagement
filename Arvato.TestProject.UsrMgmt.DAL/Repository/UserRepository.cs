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
         
            return Convert.ToBoolean(addrow);
        }
        public void Login(User entity) // Added by Ben.
        {
           // int checkuser = 0;
            try
            {
                SqlParameter[] parameters = {new SqlParameter("@LoginID",SqlDbType.NVarChar,50) {Value = entity.LoginID},
                                               new SqlParameter("@Password" , SqlDbType.NVarChar,50) {Value = entity.Password}};
                DataTable dt = executeSelectQuery("USP_USER_LOGIN", parameters);
                foreach (DataRow dr in dt.Rows)
                {
                    entity.ID = int.Parse(dr["ID"].ToString());
                    entity.FirstName = dr["FirstName"].ToString();
                    entity.LastName = dr["LastName"].ToString();
                    entity.Email = dr["Email"].ToString();
                    entity.LoginID = dr["LoginID"].ToString();
                    entity.Password = dr["Password"].ToString();

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
                SqlParameter[] parameters = {new SqlParameter("@ID",SqlDbType.TinyInt) {Value = entity.ID},
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
                SqlParameter[] parameters = {new SqlParameter("@ID",SqlDbType.TinyInt) {Value = entity.ID}};
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
                                                new SqlParameter("@ID", SqlDbType.TinyInt) { Value = ID}};
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
