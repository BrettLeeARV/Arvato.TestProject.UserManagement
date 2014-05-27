<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="InsertAsset.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.Web.UI.InsertAsset" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        RoomID:
        <asp:TextBox ID="txtroomid" runat="server"></asp:TextBox>
        <br />
        <br />
        Name:
        <asp:TextBox ID="txtname" runat="server"></asp:TextBox>
        <br />
        <br />
        <asp:CheckBox ID="CheckBox1" runat="server" Text="IsEnabled" />
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Insert" />
    
        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    
    </div>
    </form>
</body>
</html>
