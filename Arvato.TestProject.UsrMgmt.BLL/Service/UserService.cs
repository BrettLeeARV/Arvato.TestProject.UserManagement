using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.DAL.Repository;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using System.Data.SqlClient;
using System.Configuration;
namespace Arvato.TestProject.UsrMgmt.BLL.Service
{
    public class UserService : IUserService
    {
        #region Fields
        
        IUserRepository userRepository;

        #endregion

        public UserService()
        {
            userRepository = new UserRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
        }

        public List<User> GetList()
        {
            try
            {
                return userRepository.GetAll().ToList<User>();
            }
            catch (Exception ex)
            {
                //Insert error Logging/Handling Mechanism here
                throw ex;
            }
            
        }

        User IUserService.GetRecord(int id)
        {
            throw new NotImplementedException();
        }

        User IUserService.GetRecord(string loginID)
        {
            throw new NotImplementedException();
        }

        public void Save(User user)
        {
            try
            {

                #region Field Validation Logic
                //Perform business logic validation here
                
                //field Validation
                if (user.FirstName.Trim().Length == 0)
                {
                    throw (new Exception("FirstName is a required field"));
                }
                if (user.LastName.Trim().Length == 0)
                {
                    throw (new Exception("LastName is a required field"));
                }

                //Duplicate checks
                //
                //
                //

                #endregion

                if (user.ID > 0)
                {
                    //Update existing record with ID greater than zero
                    userRepository.Update(user);
                }
                else
                {
                    //No ID assigned, so create new record
                    userRepository.Add(user);
                }
            }
            catch (Exception ex)
            {
                //Insert error Logging/Handling Mechanism here
                throw ex;
            }

            
        }

        void IUserService.Delete(User user)
        {
            throw new NotImplementedException();
        }
    }
}
