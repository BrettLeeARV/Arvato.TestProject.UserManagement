using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [Serializable]
    public partial class Asset
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual int RoomID { get; set; }

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

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
    }
}
