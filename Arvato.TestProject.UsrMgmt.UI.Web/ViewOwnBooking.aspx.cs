﻿using System;
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
    public partial class ViewOwnBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RefreshGridView();
            }
            
            //Booking booking = (Booking)Session["Booking"];
           

        }
        private void RefreshGridView()
        {

            User user = (User)Session["UserSession"];
            IBookingComponent bookingservice = new BookingComponent();
            GridView1.DataSource = bookingservice.GetUserOwnBooking(user.ID);
            GridView1.DataBind();
        }

        protected void GridView1_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            try
            {
                using (IBookingComponent bookingservice = new BookingComponent())
                {
                    Booking booking = new Booking();
                    booking.ID = int.Parse(GridView1.DataKeys[Int32.Parse(e.CommandArgument.ToString())].Value.ToString());
                    bookingservice.CancelBooking(booking);
                    lblID.Text = "Your Booking has been cnaceled";
                    RefreshGridView();
                   
                }
            }
            catch (Exception ex)
            {
                
                throw new Exception (ex.Message);
            }
        }


    }
}