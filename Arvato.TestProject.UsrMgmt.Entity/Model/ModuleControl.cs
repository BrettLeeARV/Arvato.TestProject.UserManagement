using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [Serializable]
    public partial class ModuleControl
    {
        public ModuleControl()
        {
            AccessMatrices = new List<AccessMatrix>();
        }

        public virtual int ID { get; set; }
        public virtual string ModuleName { get; set; }
        public virtual string Title { get; set; }
        public virtual bool IsEnabled { get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }

        public virtual IList<AccessMatrix> AccessMatrices { get; set; }
    }
}
