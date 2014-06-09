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
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserComponent us = new UserComponent();
            IBookingComponent bookingservice = new BookingComponent();
            User myuser = new User();
           // Booking booking = new Booking();
            myuser.LoginID = txtloginid.Text;
            myuser.Password = txtpassword.Text;
            
            try
            {   
                if (us.Login(myuser))
                {
                    Session["UserSession"] = myuser;

                    Response.Redirect("InsertBooking.aspx");


                }
                else
                {
                    lblstatus.Text = "Login fail.";
                }
              
            }
            catch (Exception ex)
            {
                
                lblstatus.Text = ex.Message;
            }
        }
    }
}