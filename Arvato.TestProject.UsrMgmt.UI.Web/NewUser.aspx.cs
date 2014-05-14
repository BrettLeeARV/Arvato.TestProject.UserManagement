using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.UI.Web
{
    public partial class NewUser : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {

            IUserService userService = new UserService();
         

            User myuser = new User();
            myuser.FirstName = txtFirstName.Text;
            myuser.LastName = txtLastName.Text;
            myuser.Email = txtEmail.Text;
            myuser.LoginID = txtLoginID.Text;
            myuser.Password = txtPassword.Text;

            try
            {
                userService.Save(myuser);
                lblstatus.Text = "Success!";
            }
            catch (Exception ex)
            {

                lblstatus.Text = ex.Message;
            }
         
        }
    }
}