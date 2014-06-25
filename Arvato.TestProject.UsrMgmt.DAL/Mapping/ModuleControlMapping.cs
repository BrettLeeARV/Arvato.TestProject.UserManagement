using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using FluentNHibernate.Mapping;

namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
    public class ModuleControlMapping : ClassMap<ModuleControl>
    {
        public ModuleControlMapping()
        {
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.ModuleName);
            Map(x => x.Title);
            Map(x => x.IsEnabled);
            Map(x => x.CreatedAt);
            Map(x => x.CreatedBy);

            //HasMany(x => x.AccessMatrices).KeyColumn("ModuleControlID").Inverse().Cascade.All();
        }
    }
}
