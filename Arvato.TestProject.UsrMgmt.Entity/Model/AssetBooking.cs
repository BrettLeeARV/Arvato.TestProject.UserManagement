using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [DataContract]
    public partial class AssetBooking
    {
        [DataMember]
        public virtual int ID { get; set; }

        [DataMember]
        public virtual bool Status { get; set; }

        [DataMember]
        public virtual int AssetID { get; set; }

        [DataMember]
        public virtual int BookingID { get; set; }
    }
}
