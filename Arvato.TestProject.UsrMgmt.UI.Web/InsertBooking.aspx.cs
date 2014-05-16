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

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserService userservice = new UserService();
            User user = new User();
            Booking booking = new Booking();
            user.LoginID = txtloginid.Text;
            booking.roomID = int.Parse(txtroomid.Text);
            booking.startDate = clnstart.SelectedDate;
            booking.endDate = clnEnd.SelectedDate;
           // booking.refNum = lblBookingID.Text;
            try
            {
                userservice.AddBooking(user, booking);
                lblBookingID.Text = "You have successfully booked room your refrence number is " + booking.refNum;

            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
    }
}