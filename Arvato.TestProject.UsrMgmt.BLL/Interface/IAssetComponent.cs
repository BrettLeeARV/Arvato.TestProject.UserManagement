using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
   public interface IAssetComponent : IDisposable
    {
       List<Asset> GetList();
       void Save(Asset asset);
       void Delete(Asset asset);
       Asset SelectAsset(Asset entity);
       List<Asset> GetEnabledList();
       List<Asset> GetEnabledListByRoomID(int RoomID);
    }
}
