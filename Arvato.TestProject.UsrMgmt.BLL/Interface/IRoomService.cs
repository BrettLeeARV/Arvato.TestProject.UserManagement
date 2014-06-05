using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.BLL.Interface
{
    public interface IRoomService : IDisposable
    {
        List<Room> GetList();
        List<Room> GetEnabledList();
        void Save(Room room);
        void Delete(Room room);
        Room GetRoomByID(int RoomID);
    }
}
