using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.BLL.Service
{
    public class RoomClashException : Exception
    {
        public RoomClashException()
        {
        }

        public RoomClashException(string message)
            : base(message)
        {
        }

        public RoomClashException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public IList<Booking> Clashes { get; set; }
    }
}
