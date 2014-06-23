using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Services.Contracts.DataContract
{
    [DataContract]
    public class AssetClashFault
    {
        [DataMember]
        public IList<Booking> Clashes;
    }
}
