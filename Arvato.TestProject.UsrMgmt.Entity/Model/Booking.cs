using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [Serializable]
    public partial class Booking
    {
        public Booking()
        {
            //AssetBookings = new List<AssetBooking>();
            User = new User();
            Room = new Room();
        }

        public virtual int ID { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual String RefNum { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool IsCanceled { get; set; }
        public virtual IList<AssetBooking> AssetBookings { get; set; }
        public virtual User User { get; set; }
        public virtual Room Room { get; set; }
    }
}
