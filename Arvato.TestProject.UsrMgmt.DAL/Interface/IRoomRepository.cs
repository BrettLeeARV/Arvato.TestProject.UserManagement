using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Interface
{
    public interface IRoomRepository : IBaseRepository<Room>, IDisposable
    {
        IEnumerable<Room> GetAllEnabled();
        IEnumerable<Room> GetRoomByID(int RoomID);
    }
}
