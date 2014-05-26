using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Service;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class ViewAssetList : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IAssetService assetservice = new AssetService();
            GridView1.DataSource = assetservice.GetList();
            GridView1.DataBind();
        }
    }
}