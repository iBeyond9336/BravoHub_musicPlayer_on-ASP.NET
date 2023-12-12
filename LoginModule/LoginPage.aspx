<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="BravoHub.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="stylesheet" href="loginstyles.css" />
    <title>BravoHub</title>
    <script type="text/javascript">
        const t = setInterval(() => {
            if (navigator.userActivation.hasBeenActive) {
                const v = document.createElement('video');
                v.src = 'https://ia600701.us.archive.org/11/items/Always_with_me_Piano_Instrumental/spiritedawayAlwaysWithMePianoSpiritedAwayOST.mp3';
                v.autoplay = true;
                v.loop = true;
                v.style.position = 'fix';
                v.style.display = 'none';   
                document.body.appendChild(v);
                clearInterval(t);

            }
        }, 1000);
       
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
                    <div class="a"><a href="https://www.facebook.com/ConestogaCollege/">Facebook</a></div>
                    <div class="a"><a href="https://conestoga.desire2learn.com/d2l/home">eConestoga</a></div>
               </div>   
                </div>
            </div>
            <div class="right_panel">
                <div class="login">
                    <h2 id="neon">LOGIN</h2>
                    <div class="login_box">
                        <input type="text" class="username" placeholder="UserName" runat="server" id="LoginUsername" />
                       
                    </div>
                    <div class="login_box">
                        <input type="password" class="password" placeholder="Password" runat="server" id="LoginPassword" />
                        
                    </div>

                    <asp:Button ID="login_button" class="a" runat="server" Text="Login" OnClick="login_button_Click" />
                    <span id="userFeedback" runat="server" class="error_msg_text"></span>
                    <span></span>
                    <span></span>
                    <span></span>

                    <div class="remember">
                        <input type="checkbox" id="rememberme" runat="server"/>
                        <label for="rememberme">Remember me</label>
                    </div>
                    <div class="footer" id="neon1">
                        <div class="a" ><a href="forgotPassword.html">Forget Password</a></div>
                        <div class="a"><a href="../RegisterModule/RegisterPage.aspx">Register Here!</a></div>
                    </div>
                </div>
            </div>
        </div>
    </form>
</body>
</html>
