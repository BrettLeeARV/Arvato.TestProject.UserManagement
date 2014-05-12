<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="NewUser.aspx.cs" Inherits="Arvato.TestProject.UsrMgmt.UI.Web.NewUser" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <tr>
                <td>FirstName</td>
                <td><asp:TextBox ID="txtFirstName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>LastName</td>
                <td><asp:TextBox ID="txtLastName" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>Email</td>
                <td><asp:TextBox ID="txtEmail" runat="server"></asp:TextBox></td>
            </tr>
            <tr>
                <td>LoginID</td>
                <td><asp:TextBox ID="txtLoginID" runat="server"></asp:TextBox></td>
            </tr>
             <tr>
                <td>Gender</td>
                <td><asp:TextBox ID="txtGender" runat="server"></asp:TextBox></td>
            </tr>
        </table><br />
        <asp:Button runat="server" ID="btnSave" Text="Save" OnClick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
