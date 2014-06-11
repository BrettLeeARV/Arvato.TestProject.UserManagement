using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.Services.Contracts.DataContracts;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services.Contracts
{
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        IEnumerable<User> GetList();

        [OperationContract]
        void Save(User user);

        [OperationContract]
        void Delete(User user);

        [OperationContract]
        bool Login(User user);

        [OperationContract]
        bool IsExistingLoginID(string loginID, int userID);
    }
}
