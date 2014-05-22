using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
   public class UserMapping : ClassMap<User>
    {
       public UserMapping()
       {
          // Table("User");
           Id(x => x.ID);
           Map(x => x.FirstName);
           Map(x => x.LastName);
           Map(x => x.Email);
           Map(x => x.LoginID);
           Map(x => x.Password);
        }
    }
}
