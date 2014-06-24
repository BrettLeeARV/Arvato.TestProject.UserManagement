using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
    public class RoomMapping : ClassMap<Room>
    {
        public RoomMapping()
        {
            Id(x => x.ID);
            Map(x => x.Name);
            Map(x => x.Location);
            Map(x => x.Capacity);
            Map(x => x.IsEnabled);
            Map(x => x.CreatedBy);
            Map(x => x.ModifiedBy);
        }
    }
}
