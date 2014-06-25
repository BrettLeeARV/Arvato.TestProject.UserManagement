using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [DataContract]
    public partial class Role
    {
        public Role()
        {
            AccessMatrices = new List<AccessMatrix>();
        }

        public virtual int ID { get; set; }
        public virtual string RoleName { get; set; }
        public virtual int Status{ get; set; }
        public virtual DateTime CreatedAt { get; set; }
        public virtual string CreatedBy { get; set; }
        public virtual DateTime? UpdatedAt { get; set; }
        public virtual string UpdatedBy { get; set; }

        public virtual IList<AccessMatrix> AccessMatrices { get; set; }
    }
}
