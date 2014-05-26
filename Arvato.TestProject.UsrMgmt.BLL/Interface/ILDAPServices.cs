using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface ILDAPServices : IDisposable
    {
        bool IsAuthenticated(string Domain, string LoginId, string Password);
        bool IsExistUser(string LoginId);
    }
}
