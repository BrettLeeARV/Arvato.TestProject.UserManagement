using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class ViewBookingList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IBookingComponent bookservice = new BookingComponent();
            GridView1.DataSource = bookservice.GetList();
            GridView1.DataBind();
        }
    }
}