using System;
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
    public partial class UserListing : System.Web.UI.Page
    {
        IUserComponent userService = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                bindRecords();
            }
        }

        private void bindRecords()
        {
            using (userService = new UserComponent())
            {
                gvUsers.DataSource = userService.GetList();
                gvUsers.DataBind();
            };

            
        }

        protected void gvUsers_RowDeleting(object sender, GridViewDeleteEventArgs e)
        {
            User detail = new User();
            detail.ID = (int)gvUsers.DataKeys[e.RowIndex].Value;
            try
            {
                using (userService = new UserComponent())
                {
                    userService.Delete(detail);
                    bindRecords();
                };
            }
            catch
            { 
                
            }
        }
    }
}