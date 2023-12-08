<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegisterPage.aspx.cs" Inherits="BravoHub.RegiterPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Register Page</title>
    <link rel="stylesheet" href="registerStyles.css" />
</head>
<body>
    <div class="container">
        <form id="form1" runat="server">
            <div class="login">
                <h2>REGISTER</h2>
                <div class="login_box">
                    <input type="text" class="username" placeholder="Username" runat="server" id="RegisterUsername" />
                    <label for="username"></label>
                </div>
                <div class="login_box">
                    <input type="password" class="password" placeholder="Password" runat="server" id="RegisterPassword1" />
                    <label for="password"></label>
                </div>
                <div class="login_box">
                    <input type="password" class="password" placeholder="Confirm Password" runat="server" id="RegisterPassword2" />
                    <label for="confirm_password"></label>
                </div>
                <div id="ErrorMsg" runat="server"></div>

                <asp:Button ID="MainButton" class="a" runat="server" Text="Sign Up!" OnClick="Button1_Click" />
                <span></span>
                <span></span>
                <span></span>
                <span></span>
            </div>
        </form>
    </div>
</body>
</html>
