using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    public class AssetBooking
    {
        public virtual int ID { get; set; }
        public virtual int AssetID { get; set; }
        public virtual int Status { get; set; }
        public virtual Booking Booking { get; set; }
    }
}
