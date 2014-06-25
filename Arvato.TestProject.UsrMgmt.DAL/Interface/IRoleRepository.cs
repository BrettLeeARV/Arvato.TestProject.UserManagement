using System;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Collections.Generic;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IRoleRepository : IBaseRepository<Role>, IDisposable
    {
        IEnumerable<Role> GetAllEnabled();
        IEnumerable<Role> GetRoleByID(int RoleID);
        IEnumerable<Role> GetRoleWithAccessMatrixByID(int RoleID);
    }
}
