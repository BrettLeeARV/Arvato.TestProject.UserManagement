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
          //  User newUser = new User();
            Arvato.TestProject.UsrMgmt.Entity.Model.User myuser = new Arvato.TestProject.UsrMgmt.Entity.Model.User();
            myuser.FirstName = txtFirstName.Text;
            myuser.LastName = txtLastName.Text;
            myuser.Email = txtEmail.Text;
            myuser.LoginID = txtLoginID.Text;
            myuser.Password = txtPassword.Text;
            //Map field values into entity class
           // newUser.FirstName = txtFirstName.Text;
           // newUser.LastName = txtLastName.Text;
           // newUser.Email = txtEmail.Text;
            //newUser.LoginID = txtLoginID.Text;
           // newUser.Password = txtGender.Text;

            try
            {
                int result = Arvato.TestProject.UsrMgmt.DAL.Repository.BaseRepository.USP_INSERT_USER(myuser);
                lblstatus.Text = "Success!";
            }
            catch (Exception ex)
            {

                lblstatus.Text = ex.Message;
            }
           // userService.Save(newUser);
        }
    }
}