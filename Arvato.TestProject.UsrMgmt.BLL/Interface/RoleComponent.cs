using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.DAL.Repository;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Data.SqlClient;
using System.Configuration;

namespace Arvato.TestProject.UsrMgmt.BLL.Component
{
    public class RoleComponent : IRoleComponent, IDisposable
    {
        IRoleRepository roleRepository;

       #region Constructor
       public RoleComponent()
       {
           roleRepository = new RoleRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
       }
       #endregion

       public List<Role> GetList()
       {
           try
           {
               return roleRepository.GetAll().ToList<Role>();
           }
           catch (Exception ex)
           {
               //Insert error Logging/Handling Mechanism here
               throw ex;
           }

       }

       public List<Role> GetEnabledList()
       {
           try
           {
               return roleRepository.GetAllEnabled().ToList<Role>();
           }
           catch (Exception ex)
           {
               //Insert error Logging/Handling Mechanism here
               throw ex;
           }

       }

       public void Save(Role role)
       {
           try
           {
               if (role.ID == 0)
               {
                   roleRepository.Add(role);
               }
               else
               {
                   roleRepository.Update(role);
               }
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public void Delete(Role role)
       {
           try
           {
               roleRepository.Delete(role);
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public Role GetRoleByID(int RoleID)
       {
           try
           {
               Role role = roleRepository.GetRoleByID(RoleID).SingleOrDefault();

               return role;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public Role GetRoleWithAccessMatrixByID(int RoleID)
       {
           try
           {
               Role role = roleRepository.GetRoleWithAccessMatrixByID(RoleID).SingleOrDefault();

               return role;
           }
           catch (Exception ex)
           {
               throw ex;
           }
       }

       public List<ModuleControl> GetMenuItemsByRoleID(int RoleID)
       {
           try
           {
               Role role = roleRepository.GetRoleWithAccessMatrixByID(RoleID).SingleOrDefault();
               IList<AccessMatrix> accessMatrixList = role.AccessMatrices;
               List<ModuleControl> menuItems = new List<ModuleControl>();

               foreach (AccessMatrix accessMatrix in accessMatrixList)
               {
                   if (accessMatrix.IsActive)
                       menuItems.Add(accessMatrix.ModuleControl);
               }

               return menuItems;
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
                   roleRepository.Dispose();

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
