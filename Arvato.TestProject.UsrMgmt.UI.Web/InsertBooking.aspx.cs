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
            IUserComponent userservice = new UserComponent();
            IBookingComponent bookingservice = new BookingComponent();
            User user = new User();
            Booking booking = new Booking();

            //booking.UserID = ((User)Session["UserSession"]).ID;
            booking.UserID = ((User)Session["UserSession"]).ID;
            booking.RoomID = int.Parse(DropDownList1.SelectedValue);
            booking.StartDate = clnstart.SelectedDate;
            booking.EndDate = clnEnd.SelectedDate;
           // booking.refNum = lblBookingID.Text;
            try
            {

                bookingservice.AddBooking(booking);
                lblBookingID.Text = "You have successfully booked room your refrence number is " + "<b>" + booking.RefNum + "</b>";


            }
            catch (Exception ex)
            {
                
                throw ex;
            }

        }
    }
}