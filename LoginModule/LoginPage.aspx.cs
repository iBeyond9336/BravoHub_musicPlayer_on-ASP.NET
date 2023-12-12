﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub {
    public partial class LoginPage : Page {
        private const string MEDIA_PLAYER_PAGE = "../MediaPlayer/MediaPlayer.aspx";
        private const string ERROR_MSG_EMPTY_INPUT = "The UserName or Password can't be empty";
        private const string ERROR_MSG_INVALID_INPUT = "The UserName or Password does not match our record";

        protected LoginPage() { }
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void login_button_Click(object sender, EventArgs e) {
            bool result = ValidateUserCredentials();
            if (result) {
                // TODO: redirect to the media player page
                Response.Redirect(MEDIA_PLAYER_PAGE);
            }

            // TODO: otherwise we should display an error message to the user
            userFeedback.InnerText = ERROR_MSG_EMPTY_INPUT;
        }

        protected bool ValidateUserCredentials() {
            if (LoginUsername.Value == string.Empty) {
                return false;
            } else if (LoginPassword.Value == string.Empty) {
                return false;
            }


            return true;
        }

        protected void admin_button_Click(object sender, EventArgs e)
        {
            bool result = ValidateAdminCredentials();
            if (result)
            {
                // TODO: redirect to the madmin page
                Response.Redirect(ADMIN_PAGE);
            }

            // TODO: otherwise we should display an error message to the user
                userFeedback.InnerText = ERROR_MSG_INVALID_INPUT;

        }

        protected bool ValidateAdminCredentials()
        {
            string Adminusername = LoginUsername.Value;
            string Adminpassword = LoginPassword.Value;
            if (Adminusername == string.Empty)
            {
                return false;
            }
            else if (Adminpassword == string.Empty)
            {
                return false;
            }

            // if user is Admin
            if (Adminusername == "Edwin" && Adminpassword == "123456")
            {
                Response.Redirect(ADMIN_PAGE);
                return true;
            }
            
            return false;
            

        }

    }
}
