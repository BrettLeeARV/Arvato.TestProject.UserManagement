using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using Arvato.TestProject.UsrMgmt.DAL.Repository;
using System.Configuration;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.DAL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Service
{
   public class RoomService : IRoomService,IDisposable
    {
       IRoomRepository roomRepository;  

       #region Constructor
       public RoomService()
       {
           roomRepository = new RoomRepository(new SqlConnection(ConfigurationManager.ConnectionStrings["usrMgmtConnString"].ConnectionString));
       }
       #endregion

       public List<Room> GetList()
       {
           try
           {
               return roomRepository.GetAll().ToList<Room>();
           }
           catch (Exception ex)
           {
               //Insert error Logging/Handling Mechanism here
               throw ex;
           }

       }

       public List<Room> GetEnabledList()
       {
           try
           {
               return roomRepository.GetAllEnabled().ToList<Room>();
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
                   roomRepository.Dispose();

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
