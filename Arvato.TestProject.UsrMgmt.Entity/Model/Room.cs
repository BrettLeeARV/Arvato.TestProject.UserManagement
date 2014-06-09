using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    public partial class Room
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
        public virtual int Capacity { get; set; }
        public virtual bool IsEnabled { get; set; }
    }
}
