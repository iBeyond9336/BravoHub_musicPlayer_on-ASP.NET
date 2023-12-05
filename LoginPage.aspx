<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BravoHub.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="loginstyles.css"/>
    <title>BravoHub</title>
    <script type="text/javascript">
        function submitForm() {
            document.loginForm.submit()
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="login_page_container">
        <div class="left_panel">
            <h1>Welcome to BravoHub</h1>
            <h4>Let us to help you explore your journey</h4>
            <div class="social_logins">
                <a href="https://www.facebook.com/">Facebook</a>
                <a href="https://www.tiktok.com">Tiktok</a>
            </div>
        </div>
        <div class="right_panel">
            <h2>Login</h2>
            <h5>If you dont have a account...<span>register here!</span></h5>
            <!--<form name="loginForm" action="../index.html" method="POST">-->
                <input type="text" class="username" placeholder="UserName"/>
                <input type="password" class="password" placeholder="Password"/>
                <div class="login_fom_bottom">
                    <div>
                        <input type="checkbox" id="rememberme"/>
                        <label for="rememberme"> Remember me</label>
                    </div>
                    <a href="forgotPassword.html">forget password</a>
                </div>
                <input type="button" id="login_btn" onclick="submitForm();" value="Login" />
            <!--</form>-->

        </div>
    </div>
    </form>
</body>
</html>
