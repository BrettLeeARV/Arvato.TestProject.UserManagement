using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services.Contracts.DataContracts
{
    [DataContract]
    public class BookingsData
    {
        [DataMember]
        public DateTime Start;

        [DataMember]
        public DateTime End;

        [DataMember]
        public int UserId;

        [DataMember]
        public int RoomId;

        [DataMember]
        public bool IsCanceled;

        [DataMember]
        public IList<Booking> Bookings;
    }
}
