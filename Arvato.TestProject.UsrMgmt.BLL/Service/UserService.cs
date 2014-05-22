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
using System.Text.RegularExpressions;

namespace Arvato.TestProject.UsrMgmt.BLL.Service

{
    public class UserService : IUserService
    {
        #region Fields
        
        IUserRepository userRepository;
        IBookingRepository bookingRepository;

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
            finally
            {
                userRepository.Dispose();
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
                if (user.FirstName.Trim().Length == 0)
                {
                    throw (new Exception("FirstName is a required field"));
                }
                if (user.LastName.Trim().Length == 0)
                {
                    throw (new Exception("LastName is a required field"));
                }
                if (user.Email.Trim().Length == 0)
                {
                    throw (new Exception("Email is a required field"));
                }
                if (user.Email.Trim().Length > 0)
                {
                    Regex regex = new Regex(@"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$");
                    Match match = regex.Match(user.Email.Trim());
                    if (!match.Success)
                        throw (new Exception("Email is invalid format"));
                }
                if (user.LoginID.Trim().Length == 0)
                {
                    throw (new Exception("LoginID is a required field"));
                }
                if (user.LoginID.Trim().Length < 6)
                {
                    throw (new Exception("LoginID must at least 6 characters or more"));
                }
                else if(userRepository.IsExistingLoginID(user.LoginID.Trim(),user.ID))
                {
                    throw (new Exception("LoginID already exists"));
                }
                if (user.Password.Trim().Length == 0)
                {
                    throw (new Exception("Password is a required field"));
                }
                else if (user.Password.Trim().Length < 8)
                {
                    throw (new Exception("Password must at least 8 characters"));
                }


                if (user.ID > 0)
                {
                    userRepository.Update(user);
                }
                else
                {
                    userRepository.Add(user);
                }
                //Duplicate checks
                //
                //
                //

                #endregion

      
            }
            catch (Exception ex)
            {
                //Insert error Logging/Handling Mechanism here
                throw ex;
            }
            finally
            {
                userRepository.Dispose();
            }   
        }

        public void Login(User user)
        {
            try
            {
                userRepository.Login(user);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                userRepository.Dispose();
            }
        }
        public void Update(User user)
        {
            try
            {
                userRepository.Update(user);
            }
            catch (Exception)
            {
                
                throw;
            }
        }
       

        void IUserService.Delete(User user)
        {
            try
            {
                userRepository.Delete(user);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                userRepository.Dispose();
            }
        }


    }
}
