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
    public partial class ViewOwnBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
           //lblID.Text = Session["StartDate"].ToString();
            User user = (User)Session["UserSession"];
            Booking booking = (Booking)Session["Booking"];
            IBookingService bookingservice = new BookingService();
            GridView1.DataSource = bookingservice.GetUserOwnBooking(user.ID.ToString());
            GridView1.DataBind();

        }
    }
}