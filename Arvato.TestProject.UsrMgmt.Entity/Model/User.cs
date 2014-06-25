using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    [Serializable]
    public partial class User
    {
        public virtual int ID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string LoginID { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual bool IsWindowAuthenticate { get; set; }
        public virtual int RoleID { get; set; }

        public virtual Role Role { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is User)
            {
                return this.ID == ((User)obj).ID;
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
