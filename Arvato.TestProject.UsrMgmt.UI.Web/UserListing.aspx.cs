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
    public partial class UserListing : System.Web.UI.Page
    {
        IUserService userService = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindRecords();
            }
        }

        private void bindRecords()
        {
            userService = new UserService();

            gvUsers.DataSource = userService.GetList();
            gvUsers.DataBind();
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            User detail = new User();
            detail.ID = (int)gvUsers.DataKeys[e.RowIndex].Value;
            try
            {
                userService = new UserService();
                userService.Delete(detail);
                bindRecords();
            }
            catch
            { 
                
            }
        }
    }
}