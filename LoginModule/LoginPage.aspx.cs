using BravoHub.DatabaseModule;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BravoHub {
    public partial class LoginPage : Page {
        private const string MEDIA_PLAYER_PAGE = "../MediaPlayer/MediaPlayer.aspx";
        private const string ADMIN_PAGE = "../AdminModule/AdminPage.aspx";
        private const string ERROR_MSG_EMPTY_INPUT = "The UserName or Password can't be empty";
        protected LoginPage() { }
        protected void Page_Load(object sender, EventArgs e) {

        }

        protected void login_button_Click(object sender, EventArgs e) {
            bool result = ValidateUserCredentials();
            if (result) {
                // TODO: redirect to the media player page
                Response.Write("<script>alert('Logged in successfully');</script>"); //temporary, before redirect code set
            }

            // TODO: otherwise we should display an error message to the user
            userFeedback.InnerText = ERROR_MSG_EMPTY_INPUT;
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
            return db.CheckUserCredentials(LoginUsername.Value, LoginPassword.Value);
        }
    }
}