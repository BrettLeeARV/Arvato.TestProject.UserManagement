using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
    public partial class User
    {
        public virtual int ID { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string LoginID { get; set; }
       // public string Gender { get; set; }
        public virtual string Email { get; set; }
        public virtual string Password { get; set; }
        public virtual bool IsWindowAuthenticate { get; set; }
        //Etc

    }
}
