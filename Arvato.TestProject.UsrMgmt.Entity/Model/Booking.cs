using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Arvato.TestProject.UsrMgmt.Entity.Model
{
   public partial class Booking
    {
       public int ID { get; set; }
        public int roomID { get; set; }
        public DateTime startDate { get; set; }
        public DateTime endDate { get; set; }
        public String refNum { get; set; }
    }
}
