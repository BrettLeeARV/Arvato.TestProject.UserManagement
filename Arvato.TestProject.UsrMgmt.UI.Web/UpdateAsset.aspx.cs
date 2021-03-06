﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.BLL.Component;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class UpdateAsset : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            try
            {
                using(IAssetComponent assetservice = new AssetComponent())
                {
                    Asset myasset = new Asset();
                    myasset.ID = int.Parse(txtassetID.Text);
                    myasset.RoomID = int.Parse(txtroomid.Text);
                    myasset.Name = txtname.Text;
                    myasset.IsEnabled = CheckBox1.Checked;
                    assetservice.Save(myasset);
                }
            }
            catch (Exception)
            {
                
                throw;
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            try
            {
                using (IAssetComponent assetservice = new AssetComponent())
                {
                   Asset myasset = new Asset();
                  myasset.ID = int.Parse(txtassetID.Text);
                  myasset = assetservice.SelectAsset(myasset);
                  txtroomid.Text = myasset.RoomID.ToString();
                  txtname.Text = myasset.Name;
                  CheckBox1.Checked = myasset.IsEnabled;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

   
    }
}