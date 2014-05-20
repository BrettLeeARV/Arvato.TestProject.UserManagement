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
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                lblID.Text = Session["UserSession"].ToString();
                TextBox2.Text = Session["UserFN"].ToString();
                TextBox3.Text = Session["UserLN"].ToString();
                TextBox4.Text = Session["Email"].ToString();
                TextBox5.Text = Session["LoginID"].ToString();
                TextBox6.Text = Session["Password"].ToString();
            }

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            IUserService us = new UserService();
            User user = new User();
          
            try
            {
                user.ID = int.Parse(lblID.Text);
                user.FirstName = TextBox2.Text;
                user.LastName = TextBox3.Text;
                user.Email = TextBox4.Text;
                user.LoginID = TextBox5.Text;
                user.Password = TextBox6.Text;
                us.Update(user);
                lblstatus.Text = "Success";
            }
            catch (Exception ex)
            {

                lblstatus.Text = ex.Message;
            }
          
        }
    }
}