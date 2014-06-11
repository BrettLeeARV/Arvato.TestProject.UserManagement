using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.Services.Contracts.DataContract;

namespace Arvato.TestProject.UsrMgmt.Services.Contracts
{
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        IEnumerable<Booking> GetBookings(DateTime start, DateTime end, int userId, int roomId, bool isCanceled);

        [OperationContract]
        bool CancelBooking(Booking booking);

        [OperationContract]
        [FaultContract(typeof(RoomClashFault))]
        [FaultContract(typeof(AssetClashFault))]
        Booking SaveBooking(Booking booking);

        [OperationContract]
        IEnumerable<Booking> CheckRoomAvailability(Booking booking);

        [OperationContract]
        IEnumerable<AssetBooking> CheckAssetAvailability(Booking booking);
    }
}
