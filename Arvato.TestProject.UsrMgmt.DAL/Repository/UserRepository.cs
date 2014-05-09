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
            string sqlString = @"INSERT INTO [dbo].[User] " +
                                "(FirstName, LastName, Email, LoginID) " +
                                "VALUES " +
                                "(N'" + entity.FirstName + "', N'" + entity.LastName + "', N'" + entity.Email + "', N'" + entity.LoginID + "');";

            return executeInsertQuery(sqlString, null);
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
