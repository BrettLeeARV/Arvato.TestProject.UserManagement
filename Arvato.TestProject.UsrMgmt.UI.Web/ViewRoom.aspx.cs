﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class ViewRoom : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            IRoomComponent roomservice = new RoomComponent();
            GridView1.DataSource = roomservice.GetList();
            GridView1.DataBind();

        }
    }
}