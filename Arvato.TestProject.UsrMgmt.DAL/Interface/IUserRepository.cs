using System;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Collections.Generic;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IUserRepository : IBaseRepository<User>, IDisposable
    {

        bool Login(User entity);
        bool IsExistingLoginID(string LoginID, int ID);
        User GetUserByLoginID(string LoginID);
        IEnumerable<User> GetAllUsersWithRole();
    }
}
