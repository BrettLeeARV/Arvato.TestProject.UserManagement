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
    public partial class ViewBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnviewbooking_Click(object sender, EventArgs e)
        {
            IUserService userservice = new UserService();
            IBookingService bookingservice = new BookingService();
            User user = new User();
            Booking booking = new Booking();
            booking.refNum = txtrefnum.Text;
            try
            {
                bookingservice.ViewBooking(user, booking);
                lblID.Text = booking.ID.ToString();
                lblLoginID.Text = user.LoginID;
               txtroomid.Text = booking.roomID.ToString();
                txtstartdate.Text = booking.startDate.ToString();
                txtenddate.Text = booking.endDate.ToString();
            }
            catch (Exception ex)
            {
                if (IsPostBack)
                {
                    lblstatus.Text = ex.Message;
                }
            }
           
           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserService userservice = new UserService();
            IBookingService bookingservice = new BookingService();
            User user = new User();
            Booking booking = new Booking();
            try
            {
              
                booking.ID = int.Parse(lblID.Text);
                booking.roomID = (int.Parse)(txtroomid.Text);
                booking.startDate = DateTime.Parse(txtstartdate.Text);
                booking.endDate = DateTime.Parse(txtenddate.Text);
                bookingservice.EditBooking(user,booking);

                lblstatus.Text = "Your Booking has been update!" + booking.refNum;
               
                
                    
                
 
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            IBookingService bookingservice = new BookingService();
            Booking booking = new Booking();
            try
            {
                booking.ID = int.Parse(lblID.Text);
                bookingservice.CancelBooking(booking);
                lblstatus.Text = "Your Booking has been successfully canceled.";
            }
            catch (Exception)
            {
                
                throw;
            }
        }
    }
}