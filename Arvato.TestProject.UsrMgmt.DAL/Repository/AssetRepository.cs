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
        public AssetRepository(SqlConnection dbConnection)
            : base(dbConnection)
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
        public bool InsertAsset(Asset entitiy)
        {
            bool result = false;

            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();

                using (var session = factory.OpenSession())
                {     
                    session.Save(entitiy);
                    return true;
                }
            }
            catch (Exception)
            {
                return false;
                throw;
            }
        }
        public bool UpdateAsset(Asset entity)
        {
            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();

                using (var session = factory.OpenSession())
                {

          
                    session.Update(entity);

                    return true;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public Asset SelectAsset(Asset entity)
        {
            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();

                using (var session = factory.OpenSession())
                {

                    var asset = session.QueryOver<Asset>().Where(x => x.ID == entity.ID).SingleOrDefault(); 

                    return asset;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public IQueryable<Asset> GetAllEnabled()
        {
            try
            {
                SessionFactory sf = new SessionFactory();
                var factory = sf.CreateSessionFactory();

                using (var session = factory.OpenSession())
                {
                    var assetList = session.QueryOver<Asset>().Where(x => x.IsEnabled == true).List().OrderBy(x => x.Name);
                    return assetList.AsQueryable<Asset>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }          
        }

        public IQueryable<Asset> GetByRoomID(int RoomID)
        {
            SessionFactory sf = new SessionFactory();
            var factory = sf.CreateSessionFactory();

            using (var session = factory.OpenSession())
            {
                var assetList = session.QueryOver<Asset>().Where(x => x.IsEnabled == true && x.RoomID == RoomID).List().OrderBy(x => x.Name);

                return assetList.AsQueryable<Asset>();
            }
        }
    }
}