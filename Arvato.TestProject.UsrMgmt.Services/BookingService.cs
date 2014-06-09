using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Services.Contracts;
using Arvato.TestProject.UsrMgmt.Services.Contracts.DataContracts;
using Arvato.TestProject.UsrMgmt.BLL.Component;

namespace Arvato.TestProject.UsrMgmt.Services
{
    public class BookingService : Contracts.IBookingService
    {
        public BookingsData GetBookings(BookingsData data)
        {
            IBookingComponent component = new BookingComponent();

            data.Bookings = component.GetListByFilters(data.Start, data.End, data.UserId, data.RoomId, data.IsCanceled);

            return data;
        }
    }
}
