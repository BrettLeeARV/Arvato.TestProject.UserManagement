using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [Serializable]
    [DataContract(IsReference = true)]
    public class AssetBooking
    {
        public AssetBooking()
        {
            Asset = new Asset();
        }
        public virtual int ID { get; set; }
        //public virtual int AssetID { get; set; }
        public virtual bool Status { get; set; }
        public virtual Booking Booking { get; set; }
        public virtual Asset Asset { get; set; }
    }
}
