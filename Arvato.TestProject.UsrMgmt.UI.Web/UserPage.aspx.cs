using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class UserPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Label1.Text = Session["UserSession"].ToString();
                Label2.Text = Session["UserLN"].ToString();

            }

        }
    }
}