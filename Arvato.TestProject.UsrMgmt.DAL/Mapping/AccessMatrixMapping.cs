using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
    public class AccessMatrixMapping : ClassMap<AccessMatrix>
    {
        public AccessMatrixMapping()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.ModuleControlID);
            Map(x => x.IsActive);
            Map(x => x.CreatedAt);
            Map(x => x.CreatedBy);

            References(x => x.Role, "RoleID").Cascade.All();
            References(x => x.ModuleControl, "ModuleControlID").Cascade.All();
        }
    }
}
