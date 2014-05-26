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

namespace Arvato.TestProject.UsrMgmt.BLL.Service
{
   public class AssetService : IAssetService
    {
        #region Fields
       IAssetRepository assetRepository;
       #endregion

       #region Constructor
       public AssetService()
        {
            assetRepository = new AssetRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
        }
       #endregion
       public List<Asset> GetList()
       {
           try
           {
               return assetRepository.GetList().ToList<Asset>();
           }
           catch (Exception ex)
           {
               
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
