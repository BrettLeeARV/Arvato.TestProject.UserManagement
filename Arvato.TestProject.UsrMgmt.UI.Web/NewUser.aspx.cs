﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Component;
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

            //IUserService userService = new UserService();
         

            //User myuser = new User();
            //myuser.FirstName = txtFirstName.Text;
            //myuser.LastName = txtLastName.Text;
            //myuser.Email = txtEmail.Text;
            //myuser.LoginID = txtLoginID.Text;
            //myuser.Password = txtPassword.Text;

            //try
            //{
            //    userService.Save(myuser);
            //    lblstatus.Text = "Success!";
            //}
            //catch (Exception ex)
            //{}

            using (IUserComponent userService = new UserComponent())

            {
                User myuser = new User();
                myuser.FirstName = txtFirstName.Text;
                myuser.LastName = txtLastName.Text;
                myuser.Email = txtEmail.Text;
                myuser.LoginID = txtLoginID.Text;
                myuser.Password = txtPassword.Text;
                myuser.IsWindowAuthenticate = chkWindowAuthenticate.Checked;

                try
                {
                    userService.Save(myuser);
                    lblstatus.Text = "Success!";
                }
                catch (Exception ex)
                {
                    lblstatus.Text = ex.Message;
                    //lblstatus.Text = "The Email address is already existed!";
                }
            };

         

        }

        protected void chkWindowAuthenticate_CheckedChanged(object sender, EventArgs e)
        {
            if (chkWindowAuthenticate.Checked)
            {
                txtPassword.Text = "";
                txtPassword.Enabled = false;
            }
            else
            {
                txtPassword.Enabled = true;
            }
        }
    }
}