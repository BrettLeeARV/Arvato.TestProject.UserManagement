using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using NHibernate.SqlCommand;

namespace Arvato.TestProject.UsrMgmt.DAL.Repository
{
    public class RoleRepository : BaseRepository, IRoleRepository
    {
        static string connString = String.Empty;

        #region Constructors

        public RoleRepository()
            : base()
        {

        }

        public RoleRepository(SqlConnection dbConnection)
            : base(dbConnection)
        {
            connString = dbConnection.ConnectionString;
        }

        #endregion

        #region IBaseRepository members

        public IEnumerable<Role> GetAll()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var Fields = session.QueryOver<Role>().List<Role>();
                    return Fields.AsQueryable<Role>();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public bool Add(Role entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Save(entity);

                    return true;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void Update(Role entity)
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

        public bool Delete(Role entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    session.Delete(entity);
                    session.Flush();
                    return true;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        #endregion

        #region IRoleRepository members

        public Role SelectRole(Role entity)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var role = session.QueryOver<Role>().Where(x => x.ID == entity.ID).SingleOrDefault();
                    return role;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<Role> GetAllEnabled()
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var roleList = session.QueryOver<Role>().Where(x => x.Status == 1).OrderBy(x => x.ID).Asc.List();
                    return roleList.AsQueryable<Role>();
                }
            }
            catch
            {
                throw ;
            }
        }

        public IEnumerable<Role> GetRoleByID(int RoleID)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    var roleList = session.QueryOver<Role>().Where(x => x.Status == 1 && x.ID == RoleID).OrderBy(x => x.ID).Asc.List();

                    return roleList.AsQueryable<Role>();
                }
            }
            catch
            {
                throw;
            }
        }

        public IEnumerable<Role> GetRoleWithAccessMatrixByID(int RoleID)
        {
            try
            {
                using (var session = NHibernateHelper.OpenSession(connString))
                {
                    AccessMatrix accessMatrix = null;
                    ModuleControl moduleControl = null;

                    var roleList = session.QueryOver<Role>()
                               .Where(x => x.ID == RoleID)
                               .JoinAlias(r => r.AccessMatrices, () => accessMatrix, JoinType.LeftOuterJoin)
                               .JoinAlias(r => accessMatrix.ModuleControl, () => moduleControl, JoinType.LeftOuterJoin)
                               .Where(() => moduleControl.IsEnabled)
                               .List().Distinct();

                    return roleList.AsQueryable<Role>();
                }
            }
            catch
            {
                throw;
            }
        }
        #endregion
    }
}