using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Services.Contracts;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services
{
    public class RoomService : Contracts.IRoomService
    {
        public IEnumerable<Room> GetList(bool enabledOnly)
        {
            IRoomComponent component = new RoomComponent();
            IEnumerable<Room> results = null;
            if (enabledOnly)
            {
                results = component.GetEnabledList();
            }
            else
            {
                results = component.GetList();
            }
            return results;
        }

        public Room Save(Room room)
        {
            IRoomComponent component = new RoomComponent();
            try
            {
                component.Save(room);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
            return room;
        }

        public void Delete(Room room)
        {
            IRoomComponent component = new RoomComponent();
            try
            {
                component.Delete(room);
            }
            catch (Exception e)
            {
                throw new FaultException(e.Message);
            }
        }
    }
}
