using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
   public partial class Booking
    {
       public virtual int ID { get; set; }
       public virtual int UserID { get; set; }
        public virtual int RoomID { get; set; }
        public virtual DateTime StartDate { get; set; }
        public virtual DateTime EndDate { get; set; }
        public virtual String RefNum { get; set; }
        public virtual DateTime DateCreated { get; set; }
        public virtual bool IsCanceled { get; set; }
    }
}
