using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using System.Runtime.Serialization;

namespace Arvato.TestProject.UsrMgmt.BLL.Component
{
    public class AssetClashException : Exception
    {
        public AssetClashException()
        {
        }

        public AssetClashException(string message)
            : base(message)
        {
        }

        public AssetClashException(string message, Exception inner)
            : base(message, inner)
        {
        }

        public IList<AssetBooking> Clashes { get; set; }
    }
}
