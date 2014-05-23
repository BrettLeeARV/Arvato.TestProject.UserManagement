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
    public partial class InsertBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblloginID.Text = ((User)Session["UserSession"]).LoginID;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserService userservice = new UserService();
            IBookingService bookingservice = new BookingService();
            User user = new User();
            Booking booking = new Booking();

            booking.UserID = ((User)Session["UserSession"]).ID;
            booking.RoomID = int.Parse(DropDownList1.SelectedValue);
            booking.StartDate = clnstart.SelectedDate;
            booking.EndDate = clnEnd.SelectedDate;
           // booking.refNum = lblBookingID.Text;
            try
            {
                bookingservice.AddBooking(booking);
                lblBookingID.Text = "You have successfully booked room your refrence number is " + booking.RefNum;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
    }
}