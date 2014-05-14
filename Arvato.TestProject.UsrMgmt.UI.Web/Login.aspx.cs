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
            User myuser = new User();
            myuser.LoginID = txtloginid.Text;
            myuser.Password = txtpassword.Text;
            try
            {
                if (us.Login(myuser) > 0)
                {
                    // us.Login(myuser);
                    lblstatus.Text = "Succes";
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