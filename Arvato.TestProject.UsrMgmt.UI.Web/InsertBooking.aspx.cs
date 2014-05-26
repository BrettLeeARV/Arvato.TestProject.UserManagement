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
                User user = (User)Session["UserSession"];
                lblloginID.Text = user.ID.ToString();
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserService userservice = new UserService();
            IBookingService bookingservice = new BookingService();
            User user = new User();
            Booking booking = new Booking();
            
            user.ID = Convert.ToInt32(lblloginID.Text);
            booking.roomID = int.Parse(DropDownList1.SelectedValue);
            booking.startDate = clnstart.SelectedDate;
            booking.endDate = clnEnd.SelectedDate;
           // booking.refNum = lblBookingID.Text;
            try
            {
              
                bookingservice.AddBooking(user, booking);
                lblBookingID.Text = "You have successfully booked room your refrence number is " + "<b>" + booking.refNum + "</b>";

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
    }
}