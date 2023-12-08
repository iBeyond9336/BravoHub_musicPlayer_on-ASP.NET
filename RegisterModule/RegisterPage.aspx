<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="BravoHub.RegiterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Page</title>
    <link href="registerStyles.css" type="text/css" rel="stylesheet"/>
</head>
<body>
  
    <form id="form1" runat="server">
        <div class="login">
            <h2>REGISTER</h2>
         
        <asp:Label ID="Label1" runat="server" Text="Type your new username your username"></asp:Label>
        <input id="RegisterUsername" type="text" placeholder="i.e. " runat="server"/>
        <asp:Label ID="Label2" runat="server" Text="Type your new password"></asp:Label>
        <input id="RegisterPassword1" type="password" placeholder="password" runat="server"/>
        <asp:Label ID="Label3" runat="server" Text="Confirm your password"></asp:Label>
        <input id="RegisterPassword2" type="password" placeholder="confirm password" runat="server"/>
        <div id="ErrorMsg" runat="server"></div>

        <asp:Button ID="MainButton" runat="server" Text="Sign Up!" OnClick="Button1_Click" />
            </div>
    </form>
</body>
</html>
