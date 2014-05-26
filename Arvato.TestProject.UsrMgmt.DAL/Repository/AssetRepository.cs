using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Data.SqlClient;
using Arvato.TestProject.UsrMgmt.DAL.Interface;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
  public class AssetRepository : BaseRepository, IAssetRepository
    {
              public AssetRepository(SqlConnection dbConnection):base(dbConnection)
        {
            
        }
      public IQueryable<Asset> GetList()
      {
          try
          {
              SessionFactory sf = new SessionFactory();
              var factory = sf.CreateSessionFactory();
              using (var session = factory.OpenSession())
              {
                  var Fields = session.CreateQuery("FROM Asset").List<Asset>();
                  return Fields.AsQueryable<Asset>();
              }
          }
          catch (Exception ex)
          {
              
              throw ex;
          }
      }
    }
}
