<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertBooking.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.Web.UI.InsertBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label4" runat="server" Text="Your LoginID is:"></asp:Label>&nbsp;&nbsp;<asp:Label
            ID="lblloginID" runat="server" Text=""></asp:Label>
        <br />
        <br />
        <asp:Label ID="Label2" runat="server" Text="RoomID"></asp:Label>&nbsp;&nbsp;<asp:TextBox ID="txtroomid" runat="server"></asp:TextBox>
        <br />
        <br />
        RoomName:
        <asp:DropDownList ID="DropDownList1" runat="server" 
            DataSourceID="SqlDataSource1" DataTextField="Name" DataValueField="ID">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" 
            ConnectionString="<%$ ConnectionStrings:UserManagementConnectionString %>" 
            SelectCommand="SELECT * FROM [Room]"></asp:SqlDataSource>
        <br />
        <asp:Label runat="server" Text="StartDate"></asp:Label>&nbsp;&nbsp;<asp:Calendar ID="clnstart" runat="server"></asp:Calendar> <br /> <br />
        <asp:Label ID="Label3" runat="server" Text="EndDate"></asp:Label>&nbsp;&nbsp;<asp:Calendar ID="clnEnd" runat="server"></asp:Calendar> <br />
        <asp:Button ID="Button1" runat="server" Text="Submit" onclick="Button1_Click" /> &nbsp;<asp:Label ID="lblBookingID" runat="server" Text="Label"></asp:Label><br />

    
    </div>
    </form>
</body>
</html>
