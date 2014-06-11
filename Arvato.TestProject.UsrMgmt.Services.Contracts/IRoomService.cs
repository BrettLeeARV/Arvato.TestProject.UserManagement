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
    public interface IRoomService
    {
        [OperationContract]
        IEnumerable<Room> GetList(bool enabledOnly);

        [OperationContract]
        void Save(Room room);

        [OperationContract]
        void Delete(Room room);
    }
}
