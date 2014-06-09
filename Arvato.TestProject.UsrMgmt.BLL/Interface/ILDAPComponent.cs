using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface ILDAPComponent : IDisposable
    {
        bool IsAuthenticated(string Domain, string LoginId, string Password);
        bool IsExistUser(string LoginId);
    }
}
