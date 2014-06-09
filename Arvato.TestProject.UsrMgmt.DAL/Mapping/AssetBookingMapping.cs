using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
    class AssetBookingMapping : ClassMap<AssetBooking>
    {
        public AssetBookingMapping()
        {
            Table("AssetBooking");
            Id(x => x.ID).GeneratedBy.Identity();
            Map(x => x.Status);
            Map(x => x.AssetID);
            Map(x => x.BookingID);
        }
    }
}
