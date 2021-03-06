﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class UpdateRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = "4";

                using (IRoomComponent roomService = new RoomComponent())
                {
                    Room room = new Room();
                    room = roomService.GetRoomByID(int.Parse(lblID.Text));

                    txtName.Text = room.Name;
                    txtLocation.Text = room.Location;
                    txtCapacity.Text = room.Capacity.ToString();
                    chkIsEnabled.Checked = room.IsEnabled;
                }
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (IRoomComponent roomService = new RoomComponent())
                {
                    Room myRoom = new Room();
                    myRoom.ID = int.Parse(lblID.Text);
                    myRoom.Name = txtName.Text;
                    myRoom.Location = txtLocation.Text;
                    myRoom.Capacity = int.Parse(txtCapacity.Text);
                    myRoom.IsEnabled = chkIsEnabled.Checked;
                    roomService.Save(myRoom);
                    lblMessage.Text = "Success";

                }
            }
            catch (Exception)
            {
                lblMessage.Text = "Fail";
                throw;
            }
        }
    }
}