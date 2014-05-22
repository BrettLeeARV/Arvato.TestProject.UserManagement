using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class ViewBookingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IBookingService bookservice = new BookingService();
            GridView1.DataSource = bookservice.GetList();
            GridView1.DataBind();
        }
    }
}