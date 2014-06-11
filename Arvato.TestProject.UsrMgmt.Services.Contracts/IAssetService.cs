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
    public interface IAssetService
    {
        [OperationContract]
        IEnumerable<Asset> GetList(bool Enabled);

        [OperationContract]
        bool InsertAsset(Asset asset);

        [OperationContract]
        bool UpdateAsset(Asset entity);

       

    }
}
