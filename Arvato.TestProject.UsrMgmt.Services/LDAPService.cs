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
    public class LDAPService : Contracts.ILDAPService
    {
        public bool IsAuthenticated(User user)
        {
            ILDAPComponent component = new LDAPComponent();
            bool results = false;
            results = component.IsAuthenticated(user.LoginID, user.Password);
            return results;
        }

        public bool IsExistUser(User user)
        {
            ILDAPComponent component = new LDAPComponent();
            bool results = false;
            results = component.IsExistUser(user.LoginID);
            return results;
        }
    }
}
