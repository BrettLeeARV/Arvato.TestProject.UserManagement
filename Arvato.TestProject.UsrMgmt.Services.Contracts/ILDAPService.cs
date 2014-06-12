using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services.Contracts
{
    [ServiceContract]
    public interface ILDAPService
    {

        [OperationContract]
        bool IsAuthenticated(User user);

        [OperationContract]
        bool IsExistUser(User user);
    }
}
