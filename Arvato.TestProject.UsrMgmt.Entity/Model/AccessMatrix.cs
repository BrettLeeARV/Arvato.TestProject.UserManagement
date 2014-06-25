using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [DataContract(IsReference=true)]
    public partial class AccessMatrix
    {
        public virtual int ID { get; set; }
        public virtual int RoleID { get; set; }
        public virtual int ModuleControlID { get; set; }
        public virtual bool IsActive { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }

        public virtual Role Role { get; set; }
        public virtual ModuleControl ModuleControl { get; set; }
    }
}
