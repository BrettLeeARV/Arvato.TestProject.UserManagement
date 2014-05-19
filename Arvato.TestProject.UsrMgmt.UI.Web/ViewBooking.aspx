<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewBooking.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.Web.UI.ViewBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtrefnum" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnviewbooking" runat="server" Text="view Booking" 
            onclick="btnviewbooking_Click" /><br />
        <asp:Label ID="lblLoginID" runat="server"></asp:Label>
        &nbsp;&nbsp;
        <asp:Label ID="lblID" runat="server" Visible="False"></asp:Label>
        <br />
        <asp:TextBox ID="txtroomid" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtstartdate" runat="server"></asp:TextBox><br />
        <asp:TextBox ID="txtenddate" runat="server"></asp:TextBox>
        <br />
        <asp:Label ID="lblstatus" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Update" 
            style="height: 26px" />
        <br />

    </div>
    </form>
</body>
</html>
