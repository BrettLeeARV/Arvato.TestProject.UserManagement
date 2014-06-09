using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    public partial class AssetBooking
    {
        public virtual int ID { get; set; }
        public virtual bool Status { get; set; }
        public virtual int AssetID { get; set; }
        public virtual int BookingID { get; set; }
    }
}
