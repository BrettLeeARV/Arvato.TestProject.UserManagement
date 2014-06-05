using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
   public partial class Asset
    {
       public Asset()
       {
           Room = new Room();
       }
       public virtual int ID { get; set; }
       public virtual string Name { get; set; }
       public virtual bool IsEnabled { get; set; }
       public virtual IList<AssetBooking> AssetBooking { get; set; }
       public virtual Room Room { get; set; }

       public override bool Equals(object obj)
       {
           if (obj is Asset)
           {
               return this.ID == ((Asset)obj).ID;
           }
           else
           {
               return base.Equals(obj);
           }
       }
    }
}
