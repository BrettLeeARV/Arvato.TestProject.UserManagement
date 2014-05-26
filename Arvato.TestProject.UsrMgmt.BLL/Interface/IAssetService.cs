using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
   public interface IAssetService : IDisposable
    {
       List<Asset> GetList();
       bool InsertAsset(Asset asset);
       bool UpdateAsset(Asset entity);
       Asset SelectAsset(Asset entity);
    }
}
