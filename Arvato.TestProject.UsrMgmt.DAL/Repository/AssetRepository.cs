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
        static string connString = "";

        public AssetRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
        }

        public IQueryable<Asset> GetList()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
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
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
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
                using (var session = NHibernateHelper.OpenSession(connString))
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
                using (var session = NHibernateHelper.OpenSession(connString))
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
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var assetList = session.QueryOver<Asset>().Where(x => x.IsEnabled == true).OrderBy(x => x.Name).Asc.List();
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
            using (var session = NHibernateHelper.OpenSession(connString))
            {
                var assetList = session.QueryOver<Asset>().Where(x => x.IsEnabled == true && x.RoomID == RoomID).OrderBy(x => x.Name).Asc.List();

                return assetList.AsQueryable<Asset>();
            }
        }
    }
}