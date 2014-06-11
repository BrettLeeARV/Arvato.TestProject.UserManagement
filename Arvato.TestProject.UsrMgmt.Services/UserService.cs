using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Services.Contracts;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services
{
    public class UserService : Contracts.IUserService
    {

        public IEnumerable<User> GetList()
        {
            IUserComponent component = new UserComponent();
            var results = component.GetList();
            return results;
        }

        public User Save(User user)
        {
            IUserComponent component = new UserComponent();
            try
            {
                component.Save(user);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            return user;
        }

        public void Delete(User user)
        {
            IUserComponent component = new UserComponent();
            try
            {
                component.Delete(user);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }

        public User Login(User user)
        {
            IUserComponent component = new UserComponent();
            bool result = false;
            try
            {
                result = component.Login(user);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            if (result)
            {
                return user;
            }
            else
            {
                return null;
            }
        }

        public bool IsExistingLoginID(string loginID, int userID)
        {
            IUserComponent component = new UserComponent();
            bool result = false;
            try
            {
                result = component.IsExistingLoginID(loginID, userID);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            return result;
        }
    }
}
