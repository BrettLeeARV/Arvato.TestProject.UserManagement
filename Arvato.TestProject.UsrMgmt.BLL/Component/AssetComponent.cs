using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using System.Data.SqlClient;
using System.Configuration;
using Arvato.TestProject.UsrMgmt.DAL.Repository;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Interface;

namespace Arvato.TestProject.UsrMgmt.BLL.Component
{
   public class AssetComponent : IAssetComponent
    {
        #region Fields
       IAssetRepository assetRepository;
       #endregion

       #region Constructor
       public AssetComponent()
        {
            assetRepository = new AssetRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
        }
       #endregion
       public List<Asset> GetList()
       {
           try
           {
               return assetRepository.GetAll().ToList<Asset>();

           }
           catch (Exception ex)
           {
               
               throw ex;
           }

       }
       public bool InsertAsset(Asset asset)
       {
           try
           {
               assetRepository.Add(asset);
               return true;
           }
           catch (Exception)
           {
               
               throw;
           }
       }
       public bool UpdateAsset(Asset entity)
       {
           try
           {
               assetRepository.Update(entity);
               return true;
           }
           catch (Exception)
           {
               
               throw;
           }
       }
       public Asset SelectAsset(Asset entity)
       {
           try
           {
               return assetRepository.SelectAsset(entity);
                   
           }
           catch (Exception)
           {
               
               throw;
           }
       }

       public List<Asset> GetEnabledList()
       {
           try
           {
               return assetRepository.GetAllEnabled().ToList<Asset>();
           }
           catch (Exception ex)
           {
               //Insert error Logging/Handling Mechanism here
               throw ex;
           }

       }

       public List<Asset> GetEnabledListByRoomID(int RoomID)
       {
           try
           {
               return assetRepository.GetByRoomID(RoomID).ToList<Asset>();
           }
           catch (Exception ex)
           {
               //Insert error Logging/Handling Mechanism here
               throw ex;
           }

       }
       #region IDisposable Implementation
       private bool disposed = false;

       protected virtual void Dispose(bool disposing)
       {
           if (!this.disposed)
               if (disposing)
                   assetRepository.Dispose();

           this.disposed = true;
       }

       public void Dispose()
       {
           Dispose(true);
           GC.SuppressFinalize(this);
       }


       #endregion
    }
}
