using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Arvato.TestProject.UsrMgmt.BLL.Service;
using Arvato.TestProject.UsrMgmt.BLL.Interface;
using Arvato.TestProject.UsrMgmt.Entity.Model;

namespace Arvato.TestProject.UsrMgmt.Web.UI
{
    public partial class NewBooking : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RoomService roomServ = new RoomService();

                ddlRoom.DataSource = roomServ.GetEnabledList();
                ddlRoom.DataTextField = "Name";
                ddlRoom.DataValueField = "ID";
                ddlRoom.DataBind();

                ddlRoom.Items.Insert(0,new ListItem("- Please Select -", "", true));

                AssetService assetServ = new AssetService();

                List<Asset> assetLst = assetServ.GetEnabledList();
                Session["AllAsset"] = assetLst;

                lstAssetList.DataSource = assetLst;
                lstAssetList.DataTextField = "Name";
                lstAssetList.DataValueField = "ID";
                lstAssetList.DataBind();

            }
        }

        private void ResetAssetList()
        {
            lstAssetList.Items.Clear();

            lstSelectedAsset.Items.Clear();
        }

        protected void ddlRoom_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResetAssetList();

            List<Asset> assetLst = (List<Asset>)Session["AllAsset"];

            if (ddlRoom.SelectedIndex != 0)
            {
                List<Asset> selectedAsset = assetLst.Where(x => x.RoomID == int.Parse(ddlRoom.SelectedValue)).ToList();

                //List<Asset> selectedAsset = (List<Asset>)Filter;
                lstSelectedAsset.DataSource = selectedAsset;
                lstSelectedAsset.DataTextField = "Name";
                lstSelectedAsset.DataValueField = "ID";
                lstSelectedAsset.DataBind();

                assetLst = assetLst.FindAll(elem => !selectedAsset.Contains(elem));
            }

            lstAssetList.DataSource = assetLst;
            lstAssetList.DataTextField = "Name";
            lstAssetList.DataValueField = "ID";
            lstAssetList.DataBind();

        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            while (lstAssetList.SelectedIndex > -1)
            {

                lstSelectedAsset.Items.Add(lstAssetList.Items[lstAssetList.SelectedIndex]);
                
                lstAssetList.Items.RemoveAt(lstAssetList.SelectedIndex);
            }
        }

        protected void btnDeselect_Click(object sender, EventArgs e)
        {
            while (lstSelectedAsset.SelectedIndex > -1)
            {
                lstAssetList.Items.Add(lstSelectedAsset.Items[lstSelectedAsset.SelectedIndex]);

                lstSelectedAsset.Items.RemoveAt(lstSelectedAsset.SelectedIndex);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                BookingService bookServ = new BookingService();

                Booking detail = new Booking();

                detail.ID = 0;
                detail.IsCanceled = false;
                detail.UserID = 3;
                detail.StartDate = calStartDate.SelectedDate;
                detail.EndDate = calEndDate.SelectedDate;

                if (ddlRoom.SelectedIndex > 0)
                    detail.RoomID = int.Parse(ddlRoom.SelectedValue);

                if (lstSelectedAsset.Items.Count > 0)
                {
                    //detail.AssetBooked = new List<AssetBooking>();

                    foreach (ListItem selectedItem in lstSelectedAsset.Items)
                    {
                        AssetBooking bookAsset = new AssetBooking();
                        bookAsset.Status = true;
                        bookAsset.AssetID = int.Parse(selectedItem.Value);

                        detail.AssetBookings.Add(bookAsset);
                    }
                }

                bookServ.Save(detail);

                lblMessage.Text = "Booking created successful, your reference number is <b>" + detail.RefNum + "</b>"; 
            }
            catch (Exception ex)
            {
                lblMessage.Text = "Fail to create booking. " + ex.Message;
            }
        }

    }
}