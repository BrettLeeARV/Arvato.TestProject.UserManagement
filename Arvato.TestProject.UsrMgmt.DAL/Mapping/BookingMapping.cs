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
           Id(x => x.ID);
           Map(x => x.UserID);
           Map(x => x.roomID);
           Map(x => x.startDate);
           Map(x => x.endDate);
           Map(x => x.refNum);
           Map(x => x.DateCreated);
           Map(x => x.IsCanceled);
       }
    }
}
