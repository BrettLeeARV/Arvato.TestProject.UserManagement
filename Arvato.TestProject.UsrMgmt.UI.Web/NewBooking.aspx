<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewBooking.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.Web.UI.NewBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .style2
        {
            width: 37px;
        }
        .style3
        {
            width: 513px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>
                    <asp:Label ID="Label1" runat="server" Text="Start Date :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Calendar ID="calStartDate" runat="server"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label2" runat="server" Text="End Date"></asp:Label>
                </td>
                <td class="style3">
                    <asp:Calendar ID="calEndDate" runat="server"></asp:Calendar>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label3" runat="server" Text="Room :"></asp:Label>
                </td>
                <td class="style3">
                    <asp:DropDownList ID="ddlRoom" runat="server" Width="400px" AutoPostBack="True" 
                        onselectedindexchanged="ddlRoom_SelectedIndexChanged">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="Label4" runat="server" Text="Asset :"></asp:Label>
                </td>
                <td class="style3">
                    <table width="100%">
                        <tr>
                            <td>
                                <asp:ListBox ID="lstAssetList" runat="server" Width="230px" Height="240px" 
                                    SelectionMode="Multiple"></asp:ListBox>
                            </td>
                            <td align="center" class="style2" valign="middle">
                                <asp:Button ID="btnSelect" runat="server" Text=">" onclick="btnSelect_Click" /><br />
                                <asp:Button ID="btnDeselect" runat="server" Text="<" 
                                    onclick="btnDeselect_Click" />
                            </td>
                            <td>
                                <asp:ListBox ID="lstSelectedAsset" runat="server" Width="230px" Height="240px" 
                                    SelectionMode="Multiple"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td></td>
                <td class="style3">
                    <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    </form>
</body>
</html>
