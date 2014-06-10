using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IAssetRepository : IBaseRepository<Asset>, IDisposable
    {
        Asset SelectAsset(Asset entity);
        IEnumerable<Asset> GetAllEnabled();
        IEnumerable<Asset> GetByRoomID(int RoomID);
    }
}
