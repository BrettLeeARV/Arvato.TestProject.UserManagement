using System;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IUserRepository : IBaseRepository<User>, IDisposable
    {

        bool Login(User entity);
        bool IsExistingLoginID(string LoginID, int ID);

    }
}
