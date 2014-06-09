using System;
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
    public partial class ViewBookingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                RoomComponent roomServ = new RoomComponent();

                ddlRoom.DataSource = roomServ.GetEnabledList();
                ddlRoom.DataTextField = "Name";
                ddlRoom.DataValueField = "ID";
                ddlRoom.DataBind();

                ddlRoom.Items.Insert(0, new ListItem("- Please Select -", "", true));

                AssetComponent assetServ = new AssetComponent();

                List<Asset> assetLst = assetServ.GetEnabledList();
                Session["AllAsset"] = assetLst;

                lstAssetList.DataSource = assetLst;
                lstAssetList.DataTextField = "Name";
                lstAssetList.DataValueField = "ID";
                lstAssetList.DataBind();

                //ViewBookingDetail();
            }
        }

        private void ViewBookingDetail()
        {
            Booking book = new Booking();

            book.RefNum = txtRefNum.Text.Trim();

            BookingComponent serv = new BookingComponent();

            serv.ViewBooking(ref book);
            lblID.Text = book.ID.ToString();

            calStartDate.SelectedDate = book.StartDate;
            calEndDate.SelectedDate = book.EndDate;
            ddlRoom.SelectedValue = book.Room.ID.ToString();

            foreach (AssetBooking asset in book.AssetBookings)
            {

                for (int i = 0; i < lstAssetList.Items.Count; i++)
                {
                    if (asset.Asset.ID == int.Parse(lstAssetList.Items[i].Value))
                    {
                        lstSelectedAsset.Items.Add(lstAssetList.Items[i]);
                        lstAssetList.Items.RemoveAt(i);

                        break;
                    }
                }

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
                List<Asset> selectedAsset = assetLst.Where(x => x.Room.ID == int.Parse(ddlRoom.SelectedValue)).ToList();

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
                BookingComponent bookServ = new BookingComponent();

                Booking detail = new Booking();

                detail.ID = int.Parse(lblID.Text);
                detail.IsCanceled = false;
                //detail.UserID = 3;
                detail.User.ID = 3;
                detail.RefNum = txtRefNum.Text.Trim();
                detail.StartDate = calStartDate.SelectedDate;
                detail.EndDate = calEndDate.SelectedDate;

                if (ddlRoom.SelectedIndex > 0)
                    detail.Room.ID = int.Parse(ddlRoom.SelectedValue);
                else
                    detail.Room.ID = 0;

                if (lstSelectedAsset.Items.Count > 0)
                {
                    //detail.AssetBooked = new List<AssetBooking>();

                    foreach (ListItem selectedItem in lstSelectedAsset.Items)
                    {
                        AssetBooking bookAsset = new AssetBooking();
                        bookAsset.Status = true;
                        bookAsset.Asset.ID = int.Parse(selectedItem.Value);

                        detail.AssetBookings.Add(bookAsset);
                    }
                }

                bookServ.Save(detail);
                lblMessage.Text = "Update successful";
            }
            catch (Exception)
            {
                lblMessage.Text = "Update fail";
            }
        }

        protected void btnView_Click(object sender, EventArgs e)
        {
            ViewBookingDetail();
        }

    }
}