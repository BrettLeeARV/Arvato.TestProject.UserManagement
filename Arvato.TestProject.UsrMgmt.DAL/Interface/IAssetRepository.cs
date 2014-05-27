using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IAssetRepository : IDisposable
    {
        IQueryable<Asset> GetList();
        bool InsertAsset(Asset entity);
        bool UpdateAsset(Asset entity);
        Asset SelectAsset(Asset entity);
        IQueryable<Asset> GetAllEnabled();
        IQueryable<Asset> GetByRoomID(int RoomID);
    }
}
