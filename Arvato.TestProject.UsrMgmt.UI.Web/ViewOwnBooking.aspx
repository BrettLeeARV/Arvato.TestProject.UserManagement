﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ViewOwnBooking.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.Web.UI.ViewOwnBooking" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="lblID" runat="server"></asp:Label>
    </div>
    <asp:GridView ID="GridView1" runat="server" onrowcommand="GridView1_RowCommand" DataKeyNames="ID" 
        >
   <Columns>
   <asp:ButtonField ButtonType="Link" CommandName="CancelBooking" 
         HeaderText="Cancel Booking" Text="Cancel Booking" />

   </Columns>
    </asp:GridView>
    </form>
</body>
</html>
