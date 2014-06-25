using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
    public class RoleMapping : ClassMap<Role>
    {
        public RoleMapping()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.RoleName);
            Map(x => x.Status);
            Map(x => x.CreatedAt);
            Map(x => x.CreatedBy);
            Map(x => x.UpdatedAt);
            Map(x => x.UpdatedBy);
            HasMany(x => x.AccessMatrices).KeyColumn("RoleID").Inverse().Cascade.All();
            //HasOne(x => x.User).Constrained().ForeignKey();

        }
    }
}
