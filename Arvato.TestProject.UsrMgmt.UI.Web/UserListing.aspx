﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="UserListing.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.UI.Web.UserListing" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
    </div>
        <asp:GridView ID="gvUsers" runat="server" 
        onrowdeleting="gvUsers_RowDeleting" DataKeyNames="ID">
            <Columns>
                <asp:CommandField ShowDeleteButton="True" />
            </Columns>
        </asp:GridView>
    </form>
</body>
</html>
