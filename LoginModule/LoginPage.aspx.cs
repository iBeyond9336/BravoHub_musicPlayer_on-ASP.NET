using BravoHub.DatabaseModule;
using Google.Protobuf.WellKnownTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub {
    public partial class LoginPage : Page {
        private const string MEDIA_PLAYER_PAGE = "../MediaPlayer/MediaPlayer.aspx";
        private const string ADMIN_PAGE = "../AdminModule/AdminPage.aspx";
        private const string ERROR_MSG_INPUT = "User haven't registered";
        protected LoginPage() { }
        protected void Page_Load(object sender, EventArgs e) {
            // check if user is saved
            HttpCookie userInfoCookie = Request.Cookies["RememberMeCookie"];

            // use cookie to input user info
            if (userInfoCookie != null)
            {
                string userInfoCookieString = userInfoCookie.Value;
                string[] userInfoArray = userInfoCookieString.Split(',');
                LoginUsername.Value = userInfoArray[0];
                LoginPassword.Value = userInfoArray[1];
            }
        }

        protected void login_button_Click(object sender, EventArgs e) {
            bool result = ValidateUserCredentials();
            if (result) {
                // TODO: redirect to the media player page
                Response.Write("<script>alert('Logged in successfully');</script>"); //temporary, before redirect code set
            }
            else
            {
                // TODO: otherwise we should display an error message to the user
                userFeedback.InnerText = ERROR_MSG_INPUT;
            }
   
        }
        protected bool ValidateUserCredentials() {
            string username = LoginUsername.Value;
            string password = LoginPassword.Value;
            if (username == string.Empty) {
                return false;
            } else if (password == string.Empty) {
                return false;
            }
            
            // if user is Admin
            if (username == "Edwin" && password == "123456")
            {
                Response.Redirect(ADMIN_PAGE);
                return false;
            }

            DatabaseManager db = new DatabaseManager();
            bool result = db.CheckUserCredentials(LoginUsername.Value, LoginPassword.Value);
            if (result)
            {
                if (rememberme.Checked)
                {
                    // to save "Remember me" 
                    string token = Guid.NewGuid().ToString();   // token works like user_id
                    string[] userInfo = { LoginUsername.Value, LoginPassword.Value, token };
                    string userInfoArray = string.Join(",", userInfo);
                    // save to cookies
                    SaveCookie("RememberMeCookie", userInfoArray);
                }
                userFeedback.InnerText = null;

            }

            return result;
        }

        private void SaveCookie(string key, string value)
        {
            HttpCookie cookie = new HttpCookie(key, value);
            Response.Cookies.Add(cookie);
        }
    }
}