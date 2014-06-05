using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.Entity.Model;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class InsertRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                using (IRoomService roomService = new RoomService())
                {
                    Room myRoom = new Room();
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