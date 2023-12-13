using BravoHub.DatabaseModule;
using BravoHub.LoginModule.Controller;
using Google.Protobuf.WellKnownTypes;
using MySqlX.XDevAPI.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub {
    public partial class LoginPage : Page {
        private const string MEDIA_PLAYER_PAGE = "../MediaPlayer/MediaPlayer.aspx";
        private const string ADMIN_PAGE = "../AdminModule/AdminPage.aspx";
        private const string ERROR_MSG_INPUT = "User haven't registered";
        private const string REMEMBER_ME_COOKIE_KEY = "RememberMeCookie";

        private LoginController controller;
        protected LoginPage() {
            controller = new LoginController(this);
        }
        protected void Page_Load(object sender, EventArgs e) {
            // use cookie to input user info
            if (Request.Cookies[REMEMBER_ME_COOKIE_KEY] != null) {
                string userInfoCookieString = Request.Cookies[REMEMBER_ME_COOKIE_KEY].Value;
                string[] userInfoArray = userInfoCookieString.Split(',');
                LoginUsername.Value = userInfoArray[0];
                LoginPassword.Value = userInfoArray[1];
            }
        }

        protected void login_button_Click(object sender, EventArgs e) {
            int result = controller.CheckCredentials(LoginUsername.Value, LoginPassword.Value);
            switch(result) {
                case 0:
                    userFeedback.InnerText = ERROR_MSG_INPUT;
                    break;
                case 1:
                    Redirect();
                    break;
                case 2:
                    userFeedback.InnerText = "Username or password incorrect";
                    break;
                default:
                    userFeedback.InnerText = "Oops something bad happened!";
                    break;

            }
            
        }

        protected bool ValidateUserCredentials() {
            controller.CheckCredentials(LoginUsername.Value, LoginPassword.Value);
            return true;
        }

        private void Redirect() {
            if (rememberme.Checked && null == Request.Cookies[REMEMBER_ME_COOKIE_KEY]) {
                // save to cookies
                string[] userInfo = { LoginUsername.Value, LoginPassword.Value };
                string userInfoArray = string.Join(",", userInfo);
                SaveCookie(REMEMBER_ME_COOKIE_KEY, userInfoArray);
            } else if (!rememberme.Checked && null != Request.Cookies[REMEMBER_ME_COOKIE_KEY]) {
                // Remove the cookie;
                Request.Cookies.Remove(REMEMBER_ME_COOKIE_KEY);
            }

            controller.GetNextPagePath(LoginUsername.Value);
            userFeedback.InnerText = null;
        }

        private void SaveCookie(string key, string value) {
            HttpCookie cookie = new HttpCookie(key, value);
            Response.Cookies.Add(cookie);
        }

        public void ShowMediaPlayerPage() {
            Response.Redirect(MEDIA_PLAYER_PAGE);
        }

        public void ShowAdminPage() {
            Response.Redirect(ADMIN_PAGE);
        }


    }
}