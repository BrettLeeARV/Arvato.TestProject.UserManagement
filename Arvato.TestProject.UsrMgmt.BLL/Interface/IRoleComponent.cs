using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface IRoleComponent : IDisposable
    {
        List<Role> GetList();
        List<Role> GetEnabledList();
        void Save(Role role);
        void Delete(Role role);
        Role GetRoleByID(int roleID);
        Role GetRoleWithAccessMatrixByID(int roleID);
        List<ModuleControl> GetMenuItemsByRoleID(int roleID);
    }
}
