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
        static string connString = String.Empty;

        #region Constructors

        public AssetRepository()
            : base()
        {

        }

        public AssetRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
        }

        #endregion

        #region IBaseRepository members

        public IEnumerable<Asset> GetAll()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var Fields = session.QueryOver<Asset>().List<Asset>();
                    return Fields.AsQueryable<Asset>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add(Asset entitiy)
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
            }
        }

        public void Update(Asset entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Update(entity);
                    session.Flush();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Delete(Asset entity)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region IAssetRepository members

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

        public IEnumerable<Asset> GetAllEnabled()
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

        public IEnumerable<Asset> GetByRoomID(int RoomID)
        {
            using (var session = NHibernateHelper.OpenSession(connString))
            {
                var assetList = session.QueryOver<Asset>().Where(x => x.IsEnabled == true && x.RoomID == RoomID).OrderBy(x => x.Name).Asc.List();
                return assetList.AsQueryable<Asset>();
            }
        }

        #endregion
    }
}