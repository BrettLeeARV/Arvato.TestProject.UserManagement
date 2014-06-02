using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FluentNHibernate.Mapping;
using Arvato.TestProject.UsrMgmt.Entity.Model;


namespace Arvato.TestProject.UsrMgmt.DAL.Mapping
{
   public class BookingMapping : ClassMap<Booking>
    {
       public BookingMapping()
       {
           Table("Booking");
           Id(x => x.ID).GeneratedBy.Identity();
           Map(x => x.UserID);
           Map(x => x.RoomID);
           Map(x => x.StartDate);
           Map(x => x.EndDate);
           Map(x => x.RefNum);
           Map(x => x.DateCreated);
           Map(x => x.IsCanceled);
           HasMany(x => x.AssetBookings).KeyColumn("BookingID").Not.LazyLoad();
       }
    }
}
