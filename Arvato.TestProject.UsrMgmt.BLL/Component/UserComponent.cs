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

namespace Arvato.TestProject.UsrMgmt.BLL.Component

{
    public class UserComponent : IUserComponent, IDisposable
    {
        #region Fields
        
        IUserRepository userRepository;

        #endregion

        #region Constructor
        public UserComponent()
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

        public List<User> GetListWithRole()
        {
            try
            {
                return userRepository.GetAllUsersWithRole().ToList<User>();
            }
            catch
            {
                //Insert error Logging/Handling Mechanism here
                throw;
            }

        }

        User IUserComponent.GetRecord(int id)
        {
            throw new NotImplementedException();
        }

        User IUserComponent.GetRecord(string loginID)
        {
            try
            {
                RoleComponent roleComponent = new RoleComponent();

                User user = userRepository.GetUserByLoginID(loginID);
                Role role = roleComponent.GetRoleWithAccessMatrixByID(user.RoleID);
                user.Role = role;

                return user;
            }
            catch (Exception ex)
            {
                //Insert error Logging/Handling Mechanism here
                throw ex;
            }
        }

        public void Save(User user)
        {
            try
            {
                LDAPComponent ADService = new LDAPComponent();

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
                if (user.Password != null && user.Password.Trim().Length == 0 && user.IsWindowAuthenticate == false)
                {
                    throw (new Exception("Password is a required field"));
                }
                else if (user.Password != null && user.Password.Trim().Length < 8 && user.IsWindowAuthenticate == false)
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
                LDAPComponent ldap = new LDAPComponent();

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
       

        void IUserComponent.Delete(User user)
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

        public bool IsExistingLoginID(string LoginID, int ID)
        {
            try
            {
                return userRepository.IsExistingLoginID(LoginID, ID);
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
