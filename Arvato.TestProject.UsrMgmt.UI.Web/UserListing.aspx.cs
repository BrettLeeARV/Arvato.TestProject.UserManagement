using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;

namespace Arvato.TestProject.UsrMgmt.UI.Web
{
    public partial class UserListing : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUserService userService = new UserService();

            gvUsers.DataSource = userService.GetList();
            gvUsers.DataBind();

        }
    }
}