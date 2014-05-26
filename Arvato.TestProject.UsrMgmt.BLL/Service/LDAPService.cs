using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.DirectoryServices;
using System.DirectoryServices.AccountManagement;

namespace Arvato.TestProject.UsrMgmt.BLL.Service
{
    class LDAPService
    {
        //Check user login with AD
        public  bool IsAuthenticated(string LoginId, string Password)
        {
            Boolean status = false;

            string Domain = "ARVATO";

            string path = "";

            DirectoryEntry entry = new DirectoryEntry(path, Domain + @"\" + LoginId, Password);

            try
            {
                Object obj = entry.NativeObject;
                status = true;
            }
            catch (Exception ex)
            {
                status = false;
            }
            finally
            {
                entry.Dispose();
            }

            return status;
        }

        //Check whether login id exist in AD
        public bool IsExistUser(string LoginId)
        {
            string domain = "my.arvato-systems.com";

            using (var domainContext = new PrincipalContext(ContextType.Domain, domain))
            {
                using (var foundUser = UserPrincipal.FindByIdentity(domainContext, IdentityType.SamAccountName, LoginId))
                {
                    return foundUser != null;
                }
            }
        }
    }
}
