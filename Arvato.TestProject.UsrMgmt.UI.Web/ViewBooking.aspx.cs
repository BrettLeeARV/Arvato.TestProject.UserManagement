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
    public partial class ViewBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnviewbooking_Click(object sender, EventArgs e)
        {
            //IUserService userservice = new UserService();
            //IBookingService bookingservice = new BookingService();
            //User user = new User();
            //Booking booking = new Booking();
            //booking.RefNum = txtrefnum.Text;
            //try
            //{
            //    bookingservice.ViewBooking(ref booking);
            //    lblID.Text = booking.ID.ToString();
            //    lblLoginID.Text = user.LoginID;
            //   txtroomid.Text = booking.RoomID.ToString();
            //    txtstartdate.Text = booking.StartDate.ToString();
            //    txtenddate.Text = booking.EndDate.ToString();
            //}
            //catch (Exception ex)
            //{
            //    if (IsPostBack)
            //    {
            //        lblstatus.Text = ex.Message;
            //    }
            //}
            IBookingComponent bookingservice = new BookingComponent();
            //   User user = new User();
            Booking booking = new Booking();
            try
            {

                //booking.ID = int.Parse(lblID.Text);
                //booking.RoomID = (int.Parse)(txtroomid.Text);
                //booking.StartDate = DateTime.Parse(txtstartdate.Text);
                //booking.EndDate = DateTime.Parse(txtenddate.Text);
                booking.RefNum = txtrefnum.Text;
                bookingservice.ViewBooking(ref booking);
                txtroomid.Text = booking.RoomID.ToString();
                txtstartdate.Text = booking.StartDate.ToString();
                txtenddate.Text = booking.EndDate.ToString();


                // lblstatus.Text = "Your Booking has been update!" + booking.RefNum;





            }
            catch (Exception)
            {

                throw;
            }
           
           
        }
        protected void Button1_Click(object sender, EventArgs e)
        {
          //  IUserService userservice = new UserService();
            IBookingComponent bookingservice = new BookingComponent();
         //   User user = new User();
            Booking booking = new Booking();
            try
            {
              
                //booking.ID = int.Parse(lblID.Text);
                //booking.RoomID = (int.Parse)(txtroomid.Text);
                //booking.StartDate = DateTime.Parse(txtstartdate.Text);
                //booking.EndDate = DateTime.Parse(txtenddate.Text);
                booking.RefNum = txtrefnum.Text;
                bookingservice.ViewBooking(ref booking);
                txtroomid.Text = booking.RoomID.ToString();
                txtstartdate.Text = booking.StartDate.ToString();
                txtenddate.Text = booking.EndDate.ToString();
                

               // lblstatus.Text = "Your Booking has been update!" + booking.RefNum;
               
                
                    
                
 
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void btncancel_Click(object sender, EventArgs e)
        {
            IBookingComponent bookingservice = new BookingComponent();
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