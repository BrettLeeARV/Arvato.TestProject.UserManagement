using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [DataContract]
    public partial class Booking
    {
        public Booking()
        {
            AssetBookings = new List<AssetBooking>();
        }

        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual int UserID { get; set; }

        [DataMember]
        public virtual int RoomID { get; set; }

        [DataMember]
        public virtual DateTime StartDate { get; set; }

        [DataMember]
        public virtual DateTime EndDate { get; set; }

        [DataMember]
        public virtual String RefNum { get; set; }

        [DataMember]
        public virtual DateTime DateCreated { get; set; }

        [DataMember]
        public virtual bool IsCanceled { get; set; }

        [DataMember]
        public virtual IList<AssetBooking> AssetBookings { get; set; }
    }
}
