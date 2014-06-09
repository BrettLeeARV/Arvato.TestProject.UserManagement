using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.DAL;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.BLL.Interface;


namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class UserList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IUserComponent userService = new UserComponent();
            GridView1.DataSource = userService.GetList();
            GridView1.DataBind();
        }
    }
}