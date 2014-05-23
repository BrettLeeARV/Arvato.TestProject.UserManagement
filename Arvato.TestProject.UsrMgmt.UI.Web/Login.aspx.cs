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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserService us = new UserService();
            IBookingService bookingservice = new BookingService();
            User myuser = new User();
           // Booking booking = new Booking();
            myuser.LoginID = txtloginid.Text;
            myuser.Password = txtpassword.Text;
            
            try
            {
                us.Login(myuser);
                if (myuser.ID > 0)
                {
                    Session["UserSession"] = myuser;

                    Response.Redirect("InsertBooking.aspx");
                }
                else
                {
                    lblstatus.Text = "Can Not";
                }
              
            }
            catch (Exception ex)
            {
                
                lblstatus.Text = ex.Message;
            }
        }
    }
}