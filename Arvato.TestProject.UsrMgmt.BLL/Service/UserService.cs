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
    public class UserService : IUserService, IDisposable
    {
        #region Fields
        
        IUserRepository userRepository;
        IBookingRepository bookingRepository;

        #endregion

        #region Constructor
        public UserService()
        {
            userRepository = new UserRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
        }
        #endregion

        #region IUserService Implementations
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
                LDAPService ADService = new LDAPService();

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
                if (user.IsWindowAuthenticate == true && ADService.IsExistUser(user.LoginID) == false)
                {
                    throw (new Exception("LoginID is not valid!"));
                }
                if (user.Password.Trim().Length == 0 && user.IsWindowAuthenticate == false)
                {
                    throw (new Exception("Password is a required field"));
                }
                else if (user.Password.Trim().Length < 8 && user.IsWindowAuthenticate == false)
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
           
        }

        public bool Login(User user)
        {
            try
            {
                bool LoginStatus = false;
                LDAPService ldap = new LDAPService();

                if (userRepository.Login(user))
                    LoginStatus =  true;
                else
                {
                    int LoginAttempt = 0;

                    while (LoginAttempt < 3)
                    {
                        if (user.IsWindowAuthenticate)
                        {
                            LoginStatus = true;
                            break;
                        }
                        else
                        {
                            LoginStatus = false;
                            LoginAttempt++;
                        }
                    }
                }

                return LoginStatus;
            }
            catch (Exception)
            {
                throw;
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
           
        }
        #endregion

        #region IDisposable Implementation
        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
                if (disposing)
                    userRepository.Dispose();

            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }


        #endregion

    }
}
