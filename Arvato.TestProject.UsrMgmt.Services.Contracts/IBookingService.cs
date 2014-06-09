using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Arvato.TestProject.UsrMgmt.Services.Contracts.DataContracts;

namespace Arvato.TestProject.UsrMgmt.Services.Contracts
{
    [ServiceContract]
    public interface IBookingService
    {
        [OperationContract]
        BookingsData GetBookings(BookingsData data);
    }
}
