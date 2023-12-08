﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BravoHub.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="loginstyles.css" />
    <title>BravoHub</title>
    <script type="text/javascript">
        function submitForm() {
            alert("HI");
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login_page_container">
            <div class="left_panel">
                <div class="greeting" id="neon">
                    <h1>Welcome to BravoHub</h1>
                </div>
                <div class="image">
                    <img src="../img/3.gif" height="200" width="800" />
                </div>
                <div class="greeting1" id="neon">

                    <h4>Let us to help you explore your journey</h4>
                <div class="socialmedia">
                    <div class="a"><a href="https://www.facebook.com/">Facebook</a></div>
                    <div class="a"><a href="https://www.tiktok.com">Tiktok</a></div>
               </div>   
                </div>
            </div>
            <div class="right_panel">
                <div class="login">
                    <h2>LOGIN</h2>
                    <div class="login_box">
                        <input type="text" class="username" placeholder="UserName" runat="server" id="LoginUsername" />
                        <label for="username"></label>
                    </div>
                    <div class="login_box">
                        <input type="password" class="password" placeholder="Password" runat="server" id="LoginPassword" />
                        <label for="password"></label>
                    </div>

                    <asp:Button ID="login_button" class="a" runat="server" Text="Login" OnClick="login_button_Click" />
                    <span></span>
                    <span></span>
                    <span></span>
                    <span></span>

                    <div class="remember">
                        <input type="checkbox" id="rememberme" />
                        <label for="rememberme">Remember me</label>
                    </div>
                    <div class="fotter">
                        <div class="account"><a href="forgotPassword.html">Forget Password</a></div>
                        <div class="account"><a href="../RegisterModule/RegisterPage.aspx">Register Here!</a></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>