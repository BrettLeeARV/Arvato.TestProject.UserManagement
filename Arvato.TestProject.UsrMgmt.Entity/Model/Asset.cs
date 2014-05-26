using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
   public partial class Asset
    {
       public virtual int ID { get; set; }
       public virtual int RoomID { get; set; }
       public virtual string Name { get; set; }
       public virtual bool IsEnabled { get; set; }
    }
}
