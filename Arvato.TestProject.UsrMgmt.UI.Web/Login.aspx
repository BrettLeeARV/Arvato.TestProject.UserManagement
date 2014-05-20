<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.Web.UI.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        
        <asp:Label ID="Label1" runat="server" Text="UserLogin"></asp:Label>&nbsp;<asp:TextBox ID="txtloginid" runat="server"></asp:TextBox><br />
        <asp:Label ID="Label2" runat="server" Text="Password"></asp:Label>&nbsp;<asp:TextBox ID="txtpassword" runat="server"></asp:TextBox><br />
        <asp:Button ID="btnlogin" runat="server" Text="Login" onclick="Button1_Click" 
            style="height: 26px" />&nbsp;
        <asp:Label ID="lblstatus" runat="server"></asp:Label>

    
    </div>
    </form>
</body>
</html>
