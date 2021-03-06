﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [Serializable]
    public partial class Room
    {
        public virtual int ID { get; set; }
        public virtual string Name { get; set; }
        public virtual string Location { get; set; }
        public virtual int Capacity { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual int CreatedBy { get; set; }
        public virtual int? ModifiedBy { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Room)
            {
                return this.ID == ((Room)obj).ID;
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
